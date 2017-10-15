package a324.mobileapplication;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.ProgressBar;
import android.widget.TextView;
import android.widget.Toast;

public class SplashScreen extends AppCompatActivity {

    private ProgressBar pBar;
    private TextView textViewProg;
    private Button btn25;
    private ImageView imageValid;
    private ImageView imageInvalid;

    private int resultTF =0;
    int count = 0;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_splash_screen);
        pBar = (ProgressBar) findViewById(R.id.progressBar);
        textViewProg = (TextView) findViewById(R.id.textViewProgressReport);
        btn25 = (Button) findViewById(R.id.button25);
        imageValid = (ImageView) findViewById(R.id.imageViewValid);
        imageInvalid = (ImageView) findViewById(R.id.imageViewInvalid);



        //The button is used to manually go to every part:
        btn25.setOnClickListener(new View.OnClickListener(){
            @Override
            public void onClick(View view){

                pBar.setProgress(count);
                if(count == 0)
                {
                    connectToServer();
                    textViewProg.setText("Establishing connection...");    //starting info for user
                }
                if(count == 25)
                {
                    textViewProg.setText("Hashing file...");
                    hashing();
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

    private void connectToServer()
    {

    }

    private void hashing()
    {
        Toast.makeText(SplashScreen.this, "Hashing should happen now", Toast.LENGTH_SHORT).show();
        //t2.convertPDF();
    }

    private void sendFile()
    {

    }

    private void result()
    {
        resultTF = 1;
        if(resultTF == 1)
        imageInvalid.setVisibility(View.VISIBLE);
    }
}
