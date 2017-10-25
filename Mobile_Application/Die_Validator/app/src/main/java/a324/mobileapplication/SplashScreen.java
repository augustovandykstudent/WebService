package a324.mobileapplication;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
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
import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;
import java.io.InputStreamReader;
import java.security.MessageDigest;

import org.ksoap2.SoapEnvelope;
import org.ksoap2.serialization.SoapObject;
import org.ksoap2.serialization.SoapPrimitive;
import org.ksoap2.serialization.SoapSerializationEnvelope;
import org.ksoap2.transport.HttpTransportSE;

public class SplashScreen extends AppCompatActivity {

    private ProgressBar pBar;
    private TextView textViewProg;
    private TextView textViewDocName;
    private TextView textViewData;
    private Button btn25;
    private ImageView imageValid;
    private ImageView imageInvalid;

    private String tmpFileName = "tmp.txt";
    private String hashFileName = "hash.txt";
    private String selectedFileName = "";
    private String username = "";
    private StringBuilder buildTemp = null;
    private boolean ValidateSuccess = false;
    private boolean resultTF = false;
    private int count = 0;

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

        selectedFileName = getIntent().getStringExtra("<StringFileName>");
        username = getIntent().getStringExtra("<StringUserName>");
        textViewDocName.setText(selectedFileName);
        textViewData.setText("user: " + username);
/*
        //hash file:
        textViewProg.setText("Hashing file...");
        hashing();
        pBar.setProgress(30);
        //send file for validation:
        textViewProg.setText("Sending file...");
        boolean send = sendFile();
        pBar.setProgress(60);
        //process the result:
        result();
        pBar.setProgress(100);
        */


        //The button is used to manually go to every part:
        btn25.setOnClickListener(new View.OnClickListener(){
            @Override
            public void onClick(View view){

                pBar.setProgress(count);
                if(count == 0)
                {
                    textViewProg.setText("Hashing file...");
                    hashing();
                    pBar.setProgress(30);
                }
                if(count == 1)
                {
                    textViewProg.setText("Sending file...");
                    resultTF = sendFile();
                    pBar.setProgress(60);
                }
                if(count == 2)
                {
                    textViewProg.setText("Result...");
                    result();
                    pBar.setProgress(100);
                }
                count++;
            }
        });

    }
    //------------Start of file Hashing:
    private void hashing()
    {
        try {
            readTmpFile();
            stringBuilder();
            //Toast.makeText(SplashScreen.this,"Hashing completed", Toast.LENGTH_SHORT).show();
        } catch (IOException e) {
            e.printStackTrace();
            Toast.makeText(SplashScreen.this, "IO exception SplashScreen: " + e.getMessage(), Toast.LENGTH_SHORT).show();
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

        buildTemp = stringBuilder;  //assign to a global variable
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
    }//---------------end file hashing

    //Send the file:
    public boolean sendFile() {

        Thread SendThread = new Thread(new Runnable() {

            @Override
            public void run() {

                String SOAP_ACTION = "http://tempuri.org/Validate";
                String METHOD_NAME = "Validate";
                String NAMESPACE = "http://tempuri.org/";
                String URL = "http://block2g.somee.com/Service.asmx";

                try {
                    // make GET request to the given URL
                    SoapObject Request = new SoapObject(NAMESPACE, METHOD_NAME);
                    Request.addProperty("sHash", buildTemp.toString());

                    SoapSerializationEnvelope soapEnvelope = new SoapSerializationEnvelope(SoapEnvelope.VER11);
                    soapEnvelope.dotNet = true;
                    soapEnvelope.setOutputSoapObject(Request);
                    HttpTransportSE transport = new HttpTransportSE(URL);

                    //textViewProg.setText("Waiting for result...");

                    transport.call(SOAP_ACTION, soapEnvelope);
                    SoapObject response;
                    String strResponse;

                    try{    //WHAT RESPONSE STRING??????

                        response = (SoapObject) soapEnvelope.getResponse();
                        //strResponse = response.getProperty("ValidateResult").toString();
                        ValidateSuccess = Boolean.parseBoolean(response.getProperty("ValidateResult").toString());
                    }catch (ClassCastException e) {

                        response = (SoapObject)soapEnvelope.bodyIn;
                        //strResponse = response.getProperty("ValidateResult").toString();
                        ValidateSuccess = Boolean.parseBoolean(response.getProperty("ValidateResult").toString());
                    }
                } catch (Exception e) {
                }
            }
        });

        SendThread.start();
        try {
            SendThread.join();
        } catch (Exception e) {
        }
        return ValidateSuccess;
    }
    //Show the result picture:
    private void result()
    {

        if(resultTF == true)
            imageValid.setVisibility(View.VISIBLE);
        else
            imageInvalid.setVisibility(View.VISIBLE);
    }
}
