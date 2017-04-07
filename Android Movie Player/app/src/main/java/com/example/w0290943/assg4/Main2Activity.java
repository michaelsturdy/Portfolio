package com.example.w0290943.assg4;

import android.content.Intent;
import android.database.Cursor;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.RatingBar;
import android.widget.TextView;
import android.widget.Toast;

import com.google.android.youtube.player.YouTubePlayerView;

public class Main2Activity extends AppCompatActivity {

    private DbAdapter db;
    private String rating;
    private String name;
    private String desc;
    private String code;
    private RatingBar editRating;
    private EditText editName;
    private EditText editDesc;
    private EditText editCode;
    private Button make;




    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main2);

        db = new DbAdapter(this);
        editName = (EditText)findViewById(R.id.nameEdit);
        editDesc = (EditText)findViewById(R.id.descEdit);
        editCode = (EditText)findViewById(R.id.codeEdit);
        editRating = (RatingBar)findViewById(R.id.ratingEdit);

        make = (Button)findViewById(R.id.createBtn);
        make.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                name = editName.getText().toString();
                desc = editDesc.getText().toString();
                code = editCode.getText().toString();
                rating = String.valueOf(editRating.getRating());

                if(rating==null||rating == "")
                    rating="0";

                if (name.isEmpty()||desc.isEmpty()||code.isEmpty())
                {
                    req();

                }
                else
                {
                    insertMovie(name,rating,desc,code);
                    Intent i = new Intent(Main2Activity.this,MainActivity.class);
                    startActivity(i);
                }

            }
        });


    }



    public void req()
    {
        if (name.isEmpty()||desc.isEmpty()||code.isEmpty())
        {
            Toast.makeText(this,"all fields required",Toast.LENGTH_LONG).show();
        }
    }

    public void getMovie(int id)
    {

        db.open();
        Cursor c = db.getMovie(id);
        if(c != null) {
            c.moveToFirst();
            name=c.getString(1);
            rating=c.getString(2);
            desc=c.getString(3);
            code=c.getString(4);
        }
        else
            Toast.makeText(this,"No movie found",Toast.LENGTH_LONG).show();

        db.close();

    }

    public void insertMovie(String name,String rating, String desc, String code)
    {
        db.open();
        db.insertMovie(name,rating,desc,code);
        db.close();
        Toast.makeText(this,"Record Added",Toast.LENGTH_LONG).show();
    }
}
