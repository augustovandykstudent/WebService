package a324.mobileapplication;

import android.content.Intent;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import org.ksoap2.SoapEnvelope;
import org.ksoap2.serialization.SoapObject;
import org.ksoap2.serialization.SoapSerializationEnvelope;
import org.ksoap2.transport.HttpTransportSE;

public class LoginActivity extends AppCompatActivity {

    private Button btnLogin;
    private Button btnReg;
    private EditText etUsername;
    private EditText etPassword;
    private String username = "";
    private String password = "";
    private boolean loginSuccess = false;
    private TextView tloading;
    int id;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);

        btnLogin = (Button) findViewById(R.id.btnLogin);
        btnReg = (Button) findViewById(R.id.btnRegister);
        etUsername = (EditText) findViewById(R.id.editTextName);
        etPassword = (EditText) findViewById(R.id.editTextPass);
        tloading = (TextView) findViewById(R.id.textViewLoading);

        btnLogin.setOnClickListener((new View.OnClickListener() {
            @Override
            public void onClick(View view) {

                username = etUsername.getText().toString();
                password = etPassword.getText().toString();
                if((!username.equals("")) && (!password.equals("")))
                {
                    tloading.setText("Loading...");
                    boolean login = runLogin();    //try to login
                    tloading.setText("");
                    //Toast.makeText(LoginActivity.this, "" + id, Toast.LENGTH_SHORT).show();   //Display the values returned from service. 0 if unsuccessful login, otherwise user id
                    if(login)
                    {
                        Intent mainAct = new Intent(LoginActivity.this, MainActivity.class).putExtra("<StringName>", username);
                        startActivity(mainAct);
                    }
                    else
                        Toast.makeText(LoginActivity.this, "Unable to login", Toast.LENGTH_SHORT).show();
                    }
                else
                {
                    Toast.makeText(LoginActivity.this, "Please fill in all the information", Toast.LENGTH_SHORT).show();
                }
            }
        }));

        btnReg.setOnClickListener((new View.OnClickListener() {
            @Override
            public void onClick(View view) {

                Intent regAct = new Intent(LoginActivity.this, RegisterActivity.class);
                startActivity(regAct);
            }
        }));
    }

    public boolean runLogin() {

        Thread LoginThread = new Thread(new Runnable() {

            @Override
            public void run() {

                String SOAP_ACTION = "http://tempuri.org/Login";
                String METHOD_NAME = "Login";
                String NAMESPACE = "http://tempuri.org/";
                String URL = "http://block2g.somee.com/Service.asmx";

                try {
                    // make GET request to the given URL
                    SoapObject Request = new SoapObject(NAMESPACE, METHOD_NAME);
                    Request.addProperty("sUserName", username);
                    Request.addProperty("sPassword", password);

                    SoapSerializationEnvelope soapEnvelope = new SoapSerializationEnvelope(SoapEnvelope.VER11);
                    soapEnvelope.dotNet = true;
                    soapEnvelope.setOutputSoapObject(Request);
                    HttpTransportSE transport = new HttpTransportSE(URL);
                    transport.call(SOAP_ACTION, soapEnvelope);
                    SoapObject response;

                    try{
                        response = (SoapObject) soapEnvelope.getResponse();
                        id = Integer.parseInt(response.getProperty("LoginResult").toString());
                    }catch (ClassCastException e) {
                        response = (SoapObject)soapEnvelope.bodyIn;
                        id = Integer.parseInt(response.getProperty("LoginResult").toString());
                    }
                    loginSuccess = false;

                    if(id != 0)//0 means unsuccessful
                    {
                        loginSuccess = true;
                    }
                    else{
                        loginSuccess = false;
                    }
                } catch (Exception e) {
                }
            }
        });

        LoginThread.start();
        try {
            LoginThread.join();
        } catch (Exception e) {
        }
        return loginSuccess;
    }
}
