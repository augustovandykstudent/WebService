package a324.mobileapplication;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.database.Cursor;
import android.net.Uri;
import android.provider.OpenableColumns;;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;


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
    private String username = "";

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

        username = getIntent().getStringExtra("<StringName>");

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
                if((!selectedFileName.equals("")) && !(selectedFileName == null))
                {
                    converPdf();
                    writeSubmittedFiles();
                    Intent splashS = new Intent(MainActivity.this, SplashScreen.class).putExtra("<StringFileName>", selectedFileName);
                    splashS.putExtra("<StringUserName>", username);
                    startActivity(splashS);
                    path = "";
                    selectedFileName = "";
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

    private void writeSubmittedFiles() {    //writes the selected file's name to a file if the user tries to validate it
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
        }
    }

    private void readSubmittedFiles()   //show list of files used for validation in a toast
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
    private void showChooser()
    {
        intent = new Intent(Intent.ACTION_OPEN_DOCUMENT);
        intent.addCategory(Intent.CATEGORY_OPENABLE);
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
  //Read bytes of the pdf into a file for use when hashing (in SplashScreen)
    private void converPdf()
    {
        //tProgress.setText("\nhashFle() started\n");
        try {
            InputStream is = getContentResolver().openInputStream(uri);
            //tProgress.append("FileInputStream made\n");

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
            //readTmpFile();
            //stringBuilder();
        } catch (FileNotFoundException e) {
            e.printStackTrace();
            Toast.makeText(MainActivity.this, "FileNotFoundException", Toast.LENGTH_SHORT).show();
        } catch (IOException e) {
            e.printStackTrace();
            Toast.makeText(MainActivity.this, "IOException: " + e.getMessage(), Toast.LENGTH_SHORT).show();
            //tProgress.append(e.getMessage() +"\n");
        }
    }
}//end MainActivity class