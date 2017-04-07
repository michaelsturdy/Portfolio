package com.example.w0290943.assg4;

import android.content.Intent;
import android.database.Cursor;
import android.graphics.drawable.GradientDrawable;
import android.net.Uri;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.MediaController;
import android.widget.RatingBar;
import android.widget.TextView;
import android.widget.Toast;
import android.widget.VideoView;

import com.google.android.youtube.player.YouTubeBaseActivity;
import com.google.android.youtube.player.YouTubeInitializationResult;
import com.google.android.youtube.player.YouTubePlayer;
import com.google.android.youtube.player.YouTubePlayerView;

public class Player extends YouTubeBaseActivity implements YouTubePlayer.OnInitializedListener {
    private DbAdapter db;
    private String rating;
    private String name;
    private String desc;
    private String code;
    private String id;
    private YouTubePlayerView vid;
    private RatingBar ratingBar;
    private TextView tvname;
    private TextView tvdesc;
    private Button del;


    //link for youtube api
    //http://www.androidhive.info/2014/12/how-to-play-youtube-video-in-android-app/
    //sha1 finger print
    //0C:CB:25:21:87:14:7D:6B:D8:5D:11:85:DA:F5:42:8C:90:73:ED:B6

    //api key: AIzaSyAPyvUdexh9y9NUbbLfHC7MA195-tFZC6A

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_player);
        db = new DbAdapter(this);
        del = (Button)findViewById(R.id.delBtn);
        Intent i= getIntent();
        id=i.getStringExtra("id");
        getMovie(Integer.parseInt(id));
        vid=(YouTubePlayerView)findViewById(R.id.youtube_view);
        vid.initialize(Config.DEVELOPER_KEY,this);
        ratingBar=(RatingBar)findViewById(R.id.ratingBar);
        ratingBar.setRating(Float.parseFloat(rating));
        tvname=(TextView)findViewById(R.id.tvname);
        tvname.setText("Title: " + name);
        tvdesc=(TextView)findViewById(R.id.tvdesc);
        tvdesc.setText("Description: " + desc);

        if(getResources().getConfiguration().orientation==2)
        {
            ratingBar.setVisibility(View.INVISIBLE);
            tvdesc.setVisibility(View.INVISIBLE);
            tvname.setVisibility(View.INVISIBLE);
            del.setVisibility(View.INVISIBLE);
        }
        else
        {
            ratingBar.setVisibility(View.VISIBLE);
            tvdesc.setVisibility(View.VISIBLE);
            tvname.setVisibility(View.VISIBLE);
            del.setVisibility(View.VISIBLE);
        }

        ratingBar.setOnRatingBarChangeListener(new RatingBar.OnRatingBarChangeListener() {
            @Override
            public void onRatingChanged(RatingBar ratingBar, float v, boolean b) {
                rating=String.valueOf(ratingBar.getRating());
                db.open();
                db.updateMovie(Long.parseLong(id),name,rating,desc,code);
                db.close();
            }
        });

        del.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                deleteMovie(Integer.parseInt(id));
                Intent in = new Intent(Player.this,MainActivity.class);
                startActivity(in);
            }
        });





    }

    public void deleteMovie(int id)
    {
        db.open();
        if(db.deleteMovie(id))
            Toast.makeText(this,"Delete successful",Toast.LENGTH_LONG).show();
        else
            Toast.makeText(this,"Delete failed",Toast.LENGTH_LONG).show();

        db.close();
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

    @Override
    public void onInitializationSuccess(YouTubePlayer.Provider provider, YouTubePlayer youTubePlayer, boolean b) {

        youTubePlayer.loadVideo(code);
    }

    @Override
    public void onInitializationFailure(YouTubePlayer.Provider provider, YouTubeInitializationResult youTubeInitializationResult) {

    }
}
