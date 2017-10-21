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

    //private String path = getIntent().getStringExtra("StringName");
    private String path = "";

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

        path = getIntent().getStringExtra("<StringName>");
        //uri = getIntent().getExtras("<StringName>");

        //The button is used to manually go to every part:
        btn25.setOnClickListener(new View.OnClickListener(){
            @Override
            public void onClick(View view){

                pBar.setProgress(count);
                if(count == 0)
                {
                    textViewProg.setText("Establishing connection...");    //starting info for user
                    //connectToServer();
                }
                if(count == 25)
                {
                    textViewProg.setText("Hashing file...");
                    //hashing();
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

    private void connectToServer()  //establish a connection to the server
    {
        new JSONTask().execute(url);

    }

    private void hashing()
    {

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
