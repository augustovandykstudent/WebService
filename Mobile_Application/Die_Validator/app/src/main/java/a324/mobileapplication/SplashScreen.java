package a324.mobileapplication;
//added in manifest file(permission for internet):   <uses-permission android:name="android.permission.INTERNET"/>
import android.Manifest;
import android.content.Context;
import android.content.Intent;
import android.net.Uri;
import android.os.AsyncTask;
import android.os.ParcelFileDescriptor;
import android.service.chooser.ChooserTargetService;
import android.support.v4.content.FileProvider;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.webkit.WebChromeClient;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.ProgressBar;
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
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.security.MessageDigest;

public class SplashScreen extends AppCompatActivity {

    private ProgressBar pBar;
    private TextView textViewProg;
    private TextView textViewDocName;
    private TextView textViewData;
    private TextView textViewMessage;
    private Button btn25;
    private ImageView imageValid;
    private ImageView imageInvalid;


    private int resultTF =0;
    private int count = 0;  //used as vaules on progress bar
    private String url = "www.webaddress.co.za";    //this might be received from MainActivity
    private boolean connected = false;
    Uri uri = null;

    private String tmpFileName = "tmp.txt";
    private String hashFileName = "hash.txt";
    private String selectedFileName = "";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_splash_screen);
        pBar = (ProgressBar) findViewById(R.id.progressBar);
        textViewProg = (TextView) findViewById(R.id.textViewProgressReport);
        textViewDocName = (TextView) findViewById(R.id.textViewDocumentName);
        textViewData = (TextView) findViewById(R.id.textViewData);
        textViewMessage = (TextView) findViewById(R.id.textViewMessage);
        btn25 = (Button) findViewById(R.id.button25);
        imageValid = (ImageView) findViewById(R.id.imageViewValid);
        imageInvalid = (ImageView) findViewById(R.id.imageViewInvalid);

        selectedFileName = getIntent().getStringExtra("<StringName>");
        textViewDocName.setText(selectedFileName);

        //The button is used to manually go to every part:
        btn25.setOnClickListener(new View.OnClickListener(){
            @Override
            public void onClick(View view){

                pBar.setProgress(count);
                if(count == 0)
                {
                    textViewProg.setText("Hashing file...");
                    hashing();

                }
                if(count == 25)
                {
                    textViewProg.setText("Establishing connection...");    //starting info for user
                    //connectToServer();
                }
                if(count == 50)
                {
                    textViewProg.setText("Sending file...");
                    //sendFile();
                }
                if(count == 75)
                {
                    textViewProg.setText("Waiting for result...");
                    result();
                }
                if(count == 100)
                {
                    imageInvalid.setVisibility(View.INVISIBLE);
                    textViewProg.setText("Finished");
                    imageValid.setVisibility(View.VISIBLE);
                }
                count = count + 25; //only used for position on progress bar
            }
        });
    }

    private void hashing()
    {
        try {
            readTmpFile();
            stringBuilder();
            Toast.makeText(SplashScreen.this,"Hashing completed", Toast.LENGTH_SHORT).show();
        } catch (IOException e) {
            e.printStackTrace();
            Toast.makeText(SplashScreen.this, "IO exception SplashScreen: " + e.getMessage(), Toast.LENGTH_SHORT).show();
        }
    }

    private void connectToServer()  //establish a connection to the server
    {
        new JSONTask().execute(url);
    }

    private void sendFile()
    {

    }

    private void result()
    {
        resultTF = 1;   //1 if doc is valid - this will be sent back from the server
        if(resultTF == 1)
        imageInvalid.setVisibility(View.VISIBLE);
    }
//------------Start of file Hashing:
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
            Toast.makeText(SplashScreen.this, "tmp.txt read(10 lines): " + strBuffer.toString(), Toast.LENGTH_SHORT).show();
        }
        catch (FileNotFoundException e) {
            e.printStackTrace();
        }
        catch (IOException e) {
            e.printStackTrace();
        }
    }

    public void stringBuilder() throws IOException{
        FileInputStream fis = openFileInput(tmpFileName);
        InputStreamReader isr = new InputStreamReader(fis);
        BufferedReader reader = new BufferedReader(isr);

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

        BufferedWriter hashout = new BufferedWriter(new FileWriter(hashFile));
        hashout.write(getSha256(stringBuilder.toString()));
        hashout.close();

        readFile(hashFileName);
    }

    private String getSha256(String tmp){
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
            Toast.makeText(SplashScreen.this, fileName + " read(30chars): "+StrBuild.toString(), Toast.LENGTH_SHORT).show();

        } catch (FileNotFoundException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
    //---------------end file hashing

//Connecting to server and reading server feedback:
public class JSONTask extends AsyncTask<String, String, String>{
        @Override
        protected String doInBackground(String... params) {

            HttpURLConnection connection = null;
            BufferedReader read = null;

            try {
                URL urlCon = new URL(params[0]);
                connection = (HttpURLConnection) urlCon.openConnection();
                connection.connect();

                //connection.setRequestMethod("POST");//----------
                //connection.setDoInput(true);
                //connection.setDoOutput(true);//-------

                InputStream is = connection.getInputStream();

                read = new BufferedReader(new InputStreamReader(is));

                StringBuffer buf = new StringBuffer();

                String line = "";
                while((line = read.readLine()) != null)
                {
                    buf.append(line);
                }

                return buf.toString();

            } catch (MalformedURLException e) {
                e.printStackTrace();
            } catch (IOException e) {
                e.printStackTrace();
            }finally {
                if(connection != null)
                    connection.disconnect();
                if(read != null)
                {
                    try {
                        read.close();
                    } catch (IOException e) {
                        e.printStackTrace();
                    }
                }
            }
            return null;
        }

        @Override
        protected void onPostExecute(String result){
            super.onPostExecute(result);
            textViewData.setText(result);   //textViewData.setText(buf.toString());
        }
    }
}
