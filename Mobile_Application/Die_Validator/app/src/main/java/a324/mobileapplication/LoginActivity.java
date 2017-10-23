package a324.mobileapplication;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import org.ksoap2.SoapEnvelope;
import org.ksoap2.serialization.PropertyInfo;
import org.ksoap2.serialization.SoapObject;
import org.ksoap2.serialization.SoapPrimitive;
import org.ksoap2.serialization.SoapSerializationEnvelope;
import org.ksoap2.transport.HttpTransportSE;
import android.net.NetworkInfo;
import org.ksoap2.SoapEnvelope;
import org.xmlpull.v1.XmlPullParserException;

import java.io.IOException;

public class LoginActivity extends AppCompatActivity {

    private Button btnLogin;
    private EditText etUsername;
    private EditText etPassword;
    private String username = "";
    private String password = "";
    private String webServiceURL = "http://block2g.somee.com/Service.asmx";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);

        btnLogin = (Button) findViewById(R.id.btnLogin);
        etUsername = (EditText) findViewById(R.id.editTextName);
        etPassword = (EditText) findViewById(R.id.editTextPass);

        btnLogin.setOnClickListener((new View.OnClickListener() {
            @Override
            public void onClick(View view) {

                username = etUsername.getText().toString();
                password = etPassword.getText().toString();
                if((!username.equals("")) && (!password.equals("")))
                {
                    if(runLogin())
                        goToMainActivity();
                    else
                        Toast.makeText(LoginActivity.this, "Unable to login", Toast.LENGTH_SHORT).show();
                }
                else
                {
                    Toast.makeText(LoginActivity.this, "Please fill in all the information", Toast.LENGTH_SHORT).show();
                }

            }
        }));
    }

    private boolean runLogin()
    {

        boolean loginSuccess = false;
        /*
        Toast.makeText(LoginActivity.this, "Attempting login", Toast.LENGTH_SHORT).show();

        String soapAction = "http://tempuri.org/Login";
        String soapMethod = "CreateUser";
        String soapLink = "http://tempuri.org/";

        try {
            SoapObject soapLoginRequest = new SoapObject(soapLink, soapMethod);
            soapLoginRequest.addProperty("sUserName", username);
            soapLoginRequest.addProperty("sPassword", password);
            SoapSerializationEnvelope soapEnvelope = new SoapSerializationEnvelope(SoapEnvelope.VER11);
            soapEnvelope.dotNet = true;
            soapEnvelope.setOutputSoapObject(soapLoginRequest);
            HttpTransportSE transport = new HttpTransportSE(webServiceURL);

            transport.call(soapAction, soapEnvelope);

            SoapObject soapLoginResponse;
            String strResponse = "";

            soapLoginResponse = (SoapObject) soapEnvelope.getResponse();
            strResponse = soapLoginResponse.getProperty("LoginResult").toString();

            Toast.makeText(LoginActivity.this, "" + strResponse, Toast.LENGTH_SHORT).show();
            //if(strResponse.equals("1"))
            //{
            //    loginSuccess = true;
            //}


        } catch (IOException e) {
            e.printStackTrace();
        } catch (XmlPullParserException e) {
            e.printStackTrace();
        }
    */
        loginSuccess = true;
        return loginSuccess;

    }

    private void goToMainActivity()
    {
        Intent mainAct = new Intent(LoginActivity.this, MainActivity.class).putExtra("<StringName>", username);
        startActivity(mainAct);
    }
}
