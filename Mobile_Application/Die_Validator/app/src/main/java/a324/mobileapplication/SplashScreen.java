package a324.mobileapplication;
//added in manifest file(permission for internet):   <uses-permission android:name="android.permission.INTERNET"/>
import android.os.AsyncTask;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.ProgressBar;
import android.widget.TextView;
import android.widget.Toast;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.File;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.io.OutputStreamWriter;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;

public class SplashScreen extends AppCompatActivity {

    private ProgressBar pBar;
    private TextView textViewProg;
    private TextView textViewDocName;
    private TextView textViewData;
    private TextView textViewOtherInfo;
    private Button btn25;
    private ImageView imageValid;
    private ImageView imageInvalid;


    private int resultTF =0;
    private int count = 0;  //used as vaules on progress bar
    private String url = "http://group2.somee.com/Login.aspx";//"http://group2.somee.com/Fileuploadfrm.aspx";    //url to connect to (stachoverflow is not final url)
    String dataToSend = "";
    private boolean connected = false;

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
        btn25 = (Button) findViewById(R.id.button25);
        imageValid = (ImageView) findViewById(R.id.imageViewValid);
        imageInvalid = (ImageView) findViewById(R.id.imageViewInvalid);
        textViewOtherInfo = (TextView) findViewById(R.id.textViewOtherInfo);

        path = getIntent().getStringExtra("<StringName>");

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
                    sendFile();
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
        try{
            new JSONTask().execute(url);
        }catch (Exception e)
        {
            Toast.makeText(SplashScreen.this, "Connection error", Toast.LENGTH_SHORT).show();
        }


    }

    private void hashing()
    {
        //t2.convertPDF();
        File f = null;
        try {
            f = new File(path);
            textViewDocName.setText("Doc name: " + f.getName());
            //test2.convertPDF(f);    //not working????
        }catch (Exception e)
        {
            Toast.makeText(SplashScreen.this, "File error", Toast.LENGTH_SHORT).show();
    }
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

    public class JSONTask extends AsyncTask<String, String, String>{
        @Override
        protected String doInBackground(String... params) {



            HttpURLConnection connection = null;
            BufferedWriter writer = null;

            try {
                /*
                File f = new File(path);
                final JSONObject jObj = new JSONObject();
                try {
                    jObj.put("" + f.getName(), f.canExecute());
                    //return f.getAbsoluteFile().toString();
                    return  jObj.toString();
                } catch (JSONException e) {
                    e.printStackTrace();
                }
                */

                URL urlCon = new URL(params[0]);
                connection = (HttpURLConnection) urlCon.openConnection();

                connection.setRequestMethod("POST");
                connection.setDoInput(true);
                connection.setDoOutput(true);


                OutputStream os  = connection.getOutputStream();
                writer = new BufferedWriter(new OutputStreamWriter(os));

                dataToSend = "hashed_file";    //Username + password
                writer.write(dataToSend);//write parameter names and values
                writer.flush();
                writer.close();
                os.close();//StringBuffer buf = new StringBuffer();

                String success = "";
                if(connection.getResponseCode() == HttpURLConnection.HTTP_OK)
                {
                    success = "Success";
                    return success;
                }
                else
                {
                    success = "Failure";
                    return success;
                }

            } catch (MalformedURLException e) {
                e.printStackTrace();
            } catch (IOException e) {
                e.printStackTrace();
            }finally {
                if(connection != null)
                    connection.disconnect();
            }
            return null;
        }

        @Override
        protected void onPostExecute(String result){
            super.onPostExecute(result);
            textViewData.setText("URI: " + "http://group2.somee.com/Login.aspx");
            textViewOtherInfo.setText("" + result);
            Toast.makeText(SplashScreen.this, ""+ result, Toast.LENGTH_SHORT).show();   //display return values in a toast
        }
    }
/*
 //Old read page on url:
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
            textViewData.setText("URL: " +url);
            Toast.makeText(SplashScreen.this, ""+ result, Toast.LENGTH_SHORT).show();   //display return values in a toast
        }
    }
*/
}
