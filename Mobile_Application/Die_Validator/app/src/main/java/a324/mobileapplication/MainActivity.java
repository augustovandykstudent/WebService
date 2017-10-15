package a324.mobileapplication;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.database.Cursor;
import android.net.Uri;
import android.provider.OpenableColumns;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.webkit.MimeTypeMap;
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
import java.net.URI;

public class MainActivity extends AppCompatActivity {

    private Button btnFileChoose;
    private Button btnValidate;
    private Button btnShowValidatedFiles;
    private TextView textfileName;

    private TextView tName;
    private TextView tSize;
    private TextView tMime;

    private static final int READ_REQUEST_CODE = 42;    //used to browse for a file
    private Intent intent;
    //MainActivity mainObj = this;
    private Uri uri = null;

    //test2 t2 = new test2();     //test2.java is still in progress for app version

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
                //write a record for the "file validated" to a file on the device to reference all submitted files.
                writeSubmittedFiles();
                //SplashScreen sc = new SplashScreen();

                Intent splashS = new Intent(MainActivity.this, SplashScreen.class);  //loading screen
                startActivity(splashS);
            }
        }));

        btnShowValidatedFiles.setOnClickListener((new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                readSubmittedFiles();
            }
        }));
    }

    private void writeSubmittedFiles() {
        if (uri != null)
        {
            String uriStr = "" + uri;
            String fileName = "submitted_files";

            FileOutputStream fos;
            try {
                fos = openFileOutput(fileName, Context.MODE_APPEND);
                fos.write((uriStr + "\n").getBytes());  //currently only writes the URIs to a file
                fos.close();
                Toast.makeText(MainActivity.this, "Saving uri to file", Toast.LENGTH_SHORT).show();
            } catch (FileNotFoundException e) {
                e.printStackTrace();
            } catch (IOException e) {
                e.printStackTrace();
            }
            uri = null;
        }
    }

    private void readSubmittedFiles()
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
        //intent.setType("*/*");
        intent.setType("application/pdf");  //Only pdf's can be selected
        Toast.makeText(MainActivity.this, "Opening file browser", Toast.LENGTH_SHORT).show();
        startActivityForResult(intent, READ_REQUEST_CODE);
    }

    //Select a file, then show the URI of the file:
    @Override
    public void onActivityResult(int requestCode, int resultCode, Intent resultData) {
        if (requestCode == READ_REQUEST_CODE && resultCode == Activity.RESULT_OK) {

            //uri = null;
            if (resultData != null) {
                uri = resultData.getData();
                textfileName.setText("URI: " + uri);

                //Get the name of the file:
                Cursor returnCursor = getContentResolver().query(uri, null, null, null, null);
                int nameIndex = returnCursor.getColumnIndex(OpenableColumns.DISPLAY_NAME);
                int sizeIndex = returnCursor.getColumnIndex(OpenableColumns.SIZE);
                returnCursor.moveToFirst();
                String mimeType = getContentResolver().getType(uri);

                tName.setText("Name: " + returnCursor.getString(nameIndex));
                tSize.setText("Size: " + sizeIndex);
                tMime.setText("Mime type: " + mimeType);
            }
        }
    }
}