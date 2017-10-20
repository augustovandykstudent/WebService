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
    private TextView tSize;
    private TextView tMime;
    private TextView tPath;
    private TextView tProgress;

    private static final int READ_REQUEST_CODE = 42;    //used to browse for a file
    private Intent intent;
    private Uri uri = null;
    private String path = "";

    private String tmpFileName = "tmp.txt";
    private String hashFileName = "hash.txt";

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
        tSize = (TextView) findViewById(R.id.textViewSize);
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
                    //writeSubmittedFiles(); //this still works
                    converPdf();
                    //Intent splashS = new Intent(MainActivity.this, SplashScreen.class).putExtra("<StringName>", path);
                    //startActivity(splashS);
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
                fos.write((path + "\n").getBytes());
                fos.close();
                Toast.makeText(MainActivity.this, "Saving uri to file", Toast.LENGTH_SHORT).show();
            } catch (FileNotFoundException e) {
                e.printStackTrace();
            } catch (IOException e) {
                e.printStackTrace();
            }
            //path = "";
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

                /*
                MainActivity.grantUriPermission(MainActivity.getPackageName(), uri, Intent.FLAG_GRANT_READ_URI_PERMISSION);

                final int takeFlags = resultData.getFlags() & (Intent.FLAG_GRANT_READ_URI_PERMISSION);
                MainActivity.getContentResolver().takePersistableUriPermission(uri, takeFlags);
                */
                //Get the name of the file:
                Cursor returnCursor = getContentResolver().query(uri, null, null, null, null);
                int nameIndex = returnCursor.getColumnIndex(OpenableColumns.DISPLAY_NAME);
                int sizeIndex = returnCursor.getColumnIndex(OpenableColumns.SIZE);
                returnCursor.moveToFirst();
                String mimeType = getContentResolver().getType(uri);

                String name = returnCursor.getString(nameIndex);
                tName.setText("Name: " + returnCursor.getString(nameIndex));
                tSize.setText("Size: " + sizeIndex);
                tMime.setText("Mime type: " + mimeType);

                path = uri.getScheme() + "://" +uri.getAuthority() +"/"+ name; //Get path of file from URI
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
            //Toast.makeText(MainActivity.this, "FileInputStream made", Toast.LENGTH_SHORT).show();
            tProgress.append("FileInputStream made\n");

            //Remove the temporary file to ensure only the selected file is used in it, and not any previous files
            //FileOutputStream fosDel = openFileOutput("tmp",Context.MODE_APPEND);
            //File f = new File(System.getProperty("user.dir"),"tmp.txt");
            //fosDel.close();
            //f.delete();
            readFile(tmpFileName);    //check contents of tmp file before writing to it
            OutputStream fos = openFileOutput(tmpFileName, Context.MODE_APPEND);  //ope tmp file for editing
            byte[] buf = new byte[8192];
            int c = 0;
            while ((c = is.read(buf, 0, buf.length)) > 0) {
                fos.write(buf, 0, c);
                fos.flush();
            }
            fos.close();
            is.close();
            stringBuilder();
        } catch (FileNotFoundException e) {
            e.printStackTrace();
            Toast.makeText(MainActivity.this, "FileNotFoundException", Toast.LENGTH_SHORT).show();
        } catch (IOException e) {
            e.printStackTrace();
            Toast.makeText(MainActivity.this, "IOException", Toast.LENGTH_SHORT).show();
        }
    }

    public void stringBuilder() throws IOException{

        tProgress.append("stringBuilder() started\n");
        FileInputStream fis = openFileInput(tmpFileName);
        InputStreamReader isr = new InputStreamReader(fis);
        BufferedReader reader = new BufferedReader(isr);
        tProgress.append("Buffered reader created\n");

        //String LINER = null;//(only for testing)

        String line = null;
        StringBuilder  stringBuilder = new StringBuilder();
        String ls = System.getProperty("line.separator");
        try {
            while((line = reader.readLine()) != null) {
                stringBuilder.append(line);
                stringBuilder.append(ls);
                //LINER = line;
            }

            //tProgress.append("last line from reader\n");
            //Toast.makeText(MainActivity.this, "last line from reader: " + LINER, Toast.LENGTH_SHORT).show();
        } finally {
            reader.close();
            fis.close();
        }
        OutputStream fos = openFileOutput(hashFileName, Context.MODE_APPEND);
        fos.close();

        tProgress.append(hashFileName + " made");
       // FileInputStream fis2 = openFileInput("hash");
        //fis2.toString();
        //Filenotfound e
        File f = new File(System.getProperty("user.dir"),hashFileName);
        tProgress.append("\nhash path(abs): " + f.getAbsolutePath());
        tProgress.append("\nhash path: " + f.getPath());
        tProgress.append("\nhash read?: " + f.canRead());
        tProgress.append("\nhash write?: " + f.canWrite());
        //f.setWritable(true);

        FileOutputStream fos2;

        fos2 = new FileOutputStream("/"+hashFileName, true);
        FileWriter fWriter = new FileWriter(fos2.getFD());

        //File hashFile = null;//= new File(fis2);
        BufferedWriter hashout = new BufferedWriter(fWriter);//new FileWriter(hashFileName));  //FILENOTFOUND EXCEPTION
        hashout.write(getSha256(stringBuilder.toString()));
        hashout.close();
        tProgress.append("Buffered writer wrote to hashFile\n");



        readFile(tmpFileName);
        //System.out.println(getSha256(stringBuilder.toString()));
        //Toast.makeText(MainActivity.this, "Hello (shaw256): " +getSha256("hello"), Toast.LENGTH_SHORT).show();
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
        try {
            String mes = "";
            String message = "";
            FileInputStream fis = openFileInput(fileName);
            InputStreamReader isr = new InputStreamReader(fis);
            BufferedReader bfr = new BufferedReader(isr);
            StringBuffer strBuffer = new StringBuffer();
            //while ((message = bfr.readLine()) != null)
            //{
            for(int i = 0 ; i < 4 ; i++)
            {
                if ((mes =bfr.readLine()) != null)
                    message +=bfr.readLine();
            }
                strBuffer.append(message + "\n");
            //}
            Toast.makeText(MainActivity.this, "tmp read: "+strBuffer.toString(), Toast.LENGTH_SHORT).show();
            //tProgress.append("tmp read: " + strBuffer.toString());
        }
        catch (FileNotFoundException e) {
            e.printStackTrace();
        }
        catch (IOException e) {
            e.printStackTrace();
        }
    }
}