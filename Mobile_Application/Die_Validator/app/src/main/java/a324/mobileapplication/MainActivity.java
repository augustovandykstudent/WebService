package a324.mobileapplication;

import android.app.Activity;
import android.content.ContentResolver;
import android.content.Context;
import android.content.Intent;
import android.content.res.AssetFileDescriptor;
import android.database.Cursor;
import android.net.Uri;
import android.os.Environment;
import android.provider.MediaStore;
import android.provider.OpenableColumns;
import android.support.v4.provider.DocumentFile;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.webkit.MimeTypeMap;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.net.MalformedURLException;
import java.net.URI;
import java.net.URISyntaxException;
import java.net.URL;
import java.net.URLDecoder;
import java.security.MessageDigest;
import java.util.Set;

public class MainActivity extends AppCompatActivity {

    private Button btnFileChoose;
    private Button btnValidate;
    private Button btnShowValidatedFiles;
    private TextView textfileName;

    private TextView tName;
    private TextView tMime;
    private TextView tPath;
    private TextView tProgress;

    private static final int READ_REQUEST_CODE = 42;    //used to browse for a file
    private Intent intent;
    private Uri uri = null;
    private String path = "";   //not currently used to select a file

    private String tmpFileName = "tmp.txt";
    private String hashFileName = "hash.txt";
    private String selectedFileName = "";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        //Assign buttons and views of GUI to objects in this class:
        btnFileChoose = (Button) findViewById(R.id.btnChooseFile);
        btnValidate = (Button) findViewById(R.id.btnValidate);
        textfileName = (TextView) findViewById(R.id.textViewFileName);
        btnShowValidatedFiles = (Button) findViewById(R.id.btnShowValidatedFiles);

        tName = (TextView) findViewById(R.id.textViewName);
        tMime = (TextView) findViewById(R.id.textViewMimeType);
        tPath = (TextView) findViewById(R.id.textViewPath);
        tProgress = (TextView) findViewById(R.id.textViewProgress);

        enableButtonsClick();
    }
    //Activate buttons for clicking:
    private void enableButtonsClick() {
        btnFileChoose.setOnClickListener((new View.OnClickListener() {
            @Override
            public void onClick(View view) {

                showChooser();
            }
        }));

        btnValidate.setOnClickListener((new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                if((!path.equals("")) && !(path ==null))
                {
                    converPdf();
                    writeSubmittedFiles();
                    Intent splashS = new Intent(MainActivity.this, SplashScreen.class).putExtra("<StringName>", path);
                    startActivity(splashS);
                    path = "";
                }
            }
        }));

        btnShowValidatedFiles.setOnClickListener((new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                readSubmittedFiles();
            }
        }));
    }

    private void writeSubmittedFiles() {    //currently only writes the file paths to a file
        if (uri != null)
        {
            String fileName = "submitted_files";

            FileOutputStream fos;
            try {
                fos = openFileOutput(fileName, Context.MODE_APPEND);
                fos.write((selectedFileName + "\n").getBytes());
                fos.close();
                Toast.makeText(MainActivity.this, "Saving file name", Toast.LENGTH_SHORT).show();
            } catch (FileNotFoundException e) {
                e.printStackTrace();
            } catch (IOException e) {
                e.printStackTrace();
            }
            selectedFileName = "";
        }
    }

    private void readSubmittedFiles()   //show list of files validated in a toast
    {
        try {
            String message = "";
            FileInputStream fis = openFileInput("submitted_files");
            InputStreamReader isr = new InputStreamReader(fis);
            BufferedReader bfr = new BufferedReader(isr);
            StringBuffer strBuffer = new StringBuffer();
            while ((message = bfr.readLine()) != null)
            {
                strBuffer.append(message + "\n");
            }
            Toast.makeText(MainActivity.this, strBuffer.toString(), Toast.LENGTH_SHORT).show();
        }
        catch (FileNotFoundException e) {
            e.printStackTrace();
        }
        catch (IOException e) {
            e.printStackTrace();
        }
    }

    //Open a file explorer to select a file:
    private void showChooser() {

        intent = new Intent(Intent.ACTION_OPEN_DOCUMENT);
        intent.addCategory(Intent.CATEGORY_OPENABLE);
        //intent.addFlags(Intent.FLAG_GRANT_READ_URI_PERMISSION);
        //intent.addFlags(Intent.FLAG_GRANT_WRITE_URI_PERMISSION);
        //intent.addFlags(Intent.FLAG_GRANT_PERSISTABLE_URI_PERMISSION);
        //intent.addFlags(Intent.FLAG_GRANT_PREFIX_URI_PERMISSION);
        //intent.setData(uri);
        //intent.setType("*/*");
        intent.setType("application/pdf");  //Only pdf's can be selected
        Toast.makeText(MainActivity.this, "Opening file browser", Toast.LENGTH_SHORT).show();
        startActivityForResult(Intent.createChooser(intent,"open with..."), READ_REQUEST_CODE);
    }

    //Select a file, then show the URI of the file:
    @Override
    public void onActivityResult(int requestCode, int resultCode, Intent resultData) {
        if (requestCode == READ_REQUEST_CODE && resultCode == Activity.RESULT_OK) {
            if (resultData != null) {
                uri = resultData.getData();
                textfileName.setText("URI: " + uri);

                //Get the name of the file:
                Cursor returnCursor = getContentResolver().query(uri, null, null, null, null);
                int nameIndex = returnCursor.getColumnIndex(OpenableColumns.DISPLAY_NAME);
                returnCursor.moveToFirst();
                String mimeType = getContentResolver().getType(uri);

                selectedFileName = returnCursor.getString(nameIndex);
                tName.setText("Name: " + returnCursor.getString(nameIndex));
                tMime.setText("Mime type: " + mimeType);

                path = uri.getScheme() + "://" +uri.getAuthority() +"/"+ selectedFileName; //Get path of file from URI
                tPath.setText(path);
            }
        }
    }
  //==============================
    private void converPdf()
    {
        tProgress.setText("\nhashFle() started\n");
        try {
            InputStream is = getContentResolver().openInputStream(uri);
            tProgress.append("FileInputStream made\n");

            //overwrite the existing tmp.txt file with a new one to delete old data
            File hashFile = new File(getApplicationContext().getFilesDir(), tmpFileName);
            //hashFile.createNewFile();
            hashFile.delete();

            OutputStream fos = openFileOutput(tmpFileName, Context.MODE_APPEND);  //ope tmp file for editing
            byte[] buf = new byte[8192];
            int c = 0;
            while ((c = is.read(buf, 0, buf.length)) > 0) {
                fos.write(buf, 0, c);
                fos.flush();
            }
            fos.close();
            is.close();
            readTmpFile();
            stringBuilder();
        } catch (FileNotFoundException e) {
            e.printStackTrace();
            Toast.makeText(MainActivity.this, "FileNotFoundException", Toast.LENGTH_SHORT).show();
        } catch (IOException e) {
            e.printStackTrace();
            Toast.makeText(MainActivity.this, "IOException: " + e.getMessage(), Toast.LENGTH_SHORT).show();
            tProgress.append(e.getMessage() +"\n");
        }
    }

    private void readTmpFile()   //show list of files validated in a toast
    {
        try {
            String message = "";
            FileInputStream fis = openFileInput("tmp.txt");
            InputStreamReader isr = new InputStreamReader(fis);
            BufferedReader bfr = new BufferedReader(isr);
            StringBuffer strBuffer = new StringBuffer();
            int count = 0;
            while ((message = bfr.readLine()) != null && (count < 10))
            {
                strBuffer.append(message + "\n");
                count++;
            }
            Toast.makeText(MainActivity.this, "tmp.txt read(10 lines): " + strBuffer.toString(), Toast.LENGTH_SHORT).show();
        }
        catch (FileNotFoundException e) {
            e.printStackTrace();
        }
        catch (IOException e) {
            e.printStackTrace();
        }
    }

    public void stringBuilder() throws IOException{

        tProgress.append("stringBuilder() started\n");
        FileInputStream fis = openFileInput(tmpFileName);
        InputStreamReader isr = new InputStreamReader(fis);
        BufferedReader reader = new BufferedReader(isr);
        tProgress.append("Buffered reader created\n");

        String LINER = null;//(only for testing)

        String line = null;
        StringBuilder  stringBuilder = new StringBuilder();
        String ls = System.getProperty("line.separator");
        try {
            int count = 0;
            while((line = reader.readLine()) != null) {
                stringBuilder.append(line);
                stringBuilder.append(ls);
                if(count < 20) {    //the 1st 20 lines are used to test if they differ with different files (testing only)
                    LINER += line;
                }
                count++;
            }
        } finally {
            reader.close();
            fis.close();
        }
        File hashFile = new File(getApplicationContext().getFilesDir(), hashFileName);
        hashFile.delete();
        hashFile.createNewFile();   //Does not overwrite old file - only creates new file if it does not exist

        tProgress.append(hashFile.getPath() +"\n");
        tProgress.append("BEFORE BufferedWriter..\n");
        BufferedWriter hashout = new BufferedWriter(new FileWriter(hashFile));//fWriter);//new FileWriter(hashFileName));  //FILENOTFOUND EXCEPTION
        tProgress.append("BufferedWriter created\n");
        hashout.write(getSha256(stringBuilder.toString()));
        hashout.close();
        tProgress.append("Buffered writer wrote to hashFile\n");
        //Toast.makeText(MainActivity.this, getSha256(LINER), Toast.LENGTH_SHORT).show();

        readFile(hashFileName);
    }

    private String getSha256(String tmp){

        tProgress.append("getSha256() started\n");
        try{
            MessageDigest mstandard = MessageDigest.getInstance("SHA-256");
            mstandard.update(tmp.getBytes());
            return bytesToHex(mstandard.digest());
        } catch(Exception ex){
            throw new RuntimeException(ex);
        }
    }

    private String bytesToHex(byte[] bytes){
        StringBuffer result = new StringBuffer();
        for (byte b : bytes) result.append(Integer.toString((b & 0xff) + 0x100, 16).substring(1));
        return result.toString();
    }

    //test:
    private void readFile(String fileName)   //show some info in a file(for testing)
    {
        StringBuilder StrBuild = new StringBuilder();
        try {
            File f = new File(getApplicationContext().getFilesDir(), fileName);
            BufferedReader bufR = new BufferedReader(new FileReader(f));
            StrBuild.append(bufR.readLine());
            bufR.close();
            Toast.makeText(MainActivity.this, fileName + " read(30chars): "+StrBuild.toString(), Toast.LENGTH_SHORT).show();

        } catch (FileNotFoundException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}//end MainActivity class