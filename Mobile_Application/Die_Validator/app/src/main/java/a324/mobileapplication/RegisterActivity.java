package a324.mobileapplication;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import org.ksoap2.SoapEnvelope;
import org.ksoap2.SoapFault;
import org.ksoap2.serialization.PropertyInfo;
import org.ksoap2.serialization.SoapObject;
import org.ksoap2.serialization.SoapPrimitive;
import org.ksoap2.serialization.SoapSerializationEnvelope;
import org.ksoap2.transport.HttpTransportSE;
import org.xmlpull.v1.XmlPullParserException;

import java.io.IOException;

public class RegisterActivity extends AppCompatActivity {

    private Button btnReg;
    private EditText etUsername;
    private EditText etPassword;
    private EditText etEmail;
    private String username = "";
    private String password = "";
    private String email = "";
    private TextView tloading;
    private boolean regSuccess = false;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_register);

        btnReg = (Button) findViewById(R.id.btnRegister);
        etUsername = (EditText) findViewById(R.id.editTextName);
        etPassword = (EditText) findViewById(R.id.editTextPass);
        etEmail = (EditText) findViewById(R.id.editTextEmail);
        tloading = (TextView) findViewById(R.id.textViewLoading);

        btnReg.setOnClickListener((new View.OnClickListener() {
            @Override
            public void onClick(View view) {

                username = etUsername.getText().toString();
                password = etPassword.getText().toString();
                email = etEmail.getText().toString();

                if((!username.equals("")) && (!password.equals("")) && (!email.equals("")))
                {
                    tloading.setText("Loading...");
                    boolean reg = false;//runReg();    //try to login
                    tloading.setText("");

                    if(reg)
                    {
                        Toast.makeText(RegisterActivity.this, "You have been registered", Toast.LENGTH_LONG).show();
                    }
                    else
                        Toast.makeText(RegisterActivity.this, "Unable to Register", Toast.LENGTH_SHORT).show();
                }
                else
                {
                    Toast.makeText(RegisterActivity.this, "Please fill in all the information", Toast.LENGTH_SHORT).show();
                }

            }
        }));
    }

    public boolean runReg() {

        Thread LoginThread = new Thread(new Runnable() {

            @Override
            public void run() {

                String SOAP_ACTION = "http://tempuri.org/CreateUser";
                String METHOD_NAME = "CreateUser";
                String NAMESPACE = "http://tempuri.org/";
                String URL = "http://block2g.somee.com/Service.asmx";

                try {
                    // make GET request to the given URL
                    SoapObject Request = new SoapObject(NAMESPACE, METHOD_NAME);
                    Request.addProperty("sUserName", username);
                    Request.addProperty("sPassword", password);
                    Request.addProperty("sEmail", email);

                    SoapSerializationEnvelope soapEnvelope = new SoapSerializationEnvelope(SoapEnvelope.VER11);
                    soapEnvelope.dotNet = true;
                    soapEnvelope.setOutputSoapObject(Request);
                    HttpTransportSE transport = new HttpTransportSE(URL);
                    transport.call(SOAP_ACTION, soapEnvelope);
                    SoapObject response;
                    String strResponse;

                    try{

                        response = (SoapObject) soapEnvelope.getResponse();
                        strResponse = response.getProperty("LoginResult").toString();
                    }catch (ClassCastException e) {

                        response = (SoapObject)soapEnvelope.bodyIn;
                        strResponse = response.getProperty("LoginResult").toString();
                    }

                    regSuccess = false;

                    if(strResponse.equals("true"))
                    {
                        regSuccess = true;
                    }
                    else{
                        regSuccess = false;
                    }
                } catch (Exception e) {
                }
            }
        });

        LoginThread.start();
        try {
            LoginThread.join();
        } catch (Exception e) {;
        }
        return regSuccess;
    }
}
