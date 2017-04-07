package com.example.w0290943.assg4;

import android.content.Context;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.support.v7.app.AppCompatActivity;
import android.os.*;
//import android.view.*;
//import android.app.Activity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.*;
import android.database.*;//for cursor
import java.io.*;
import java.net.URL;
import java.util.ArrayList;
import java.util.List;

public class MainActivity extends AppCompatActivity {

    private ListView list;
    private List<String> idList = new ArrayList<>();//used to pass id to next activity
    private List<String> nameList = new ArrayList<>();//populates titles
    private List<String> ratingList = new ArrayList<>();//not needed here
    private List<String> descriptionList = new ArrayList<>();//not needed here
    private List<String> codeList = new ArrayList<>();//populates thumbnails
    private DbAdapter db;
    private Button modify;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder().permitAll().build();
        StrictMode.setThreadPolicy(policy);

        try{
            String destPath = "/data/data/" + getPackageName() +"/database/DB";
            File f = new File(destPath);
            if(!f.exists()){
                CopyDB(getBaseContext().getAssets().open("db"),
                        new FileOutputStream(destPath));
            }

        }catch (FileNotFoundException e){
            e.printStackTrace();
        }catch (IOException e){
            e.printStackTrace();
        }



        db = new DbAdapter(this);
        list =(ListView) findViewById(R.id.listView);
        list.setAdapter(new myadapter());
        list.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {

                getMovie(Integer.parseInt(idList.get(position)));
                Intent intent = new Intent(MainActivity.this,Player.class);
                intent.putExtra("id",idList.get(position));
                startActivity(intent);

            }
        });

        modify=(Button)findViewById(R.id.modifyBtn);
        modify.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent i = new Intent(MainActivity.this,Main2Activity.class);
                startActivity(i);

            }
        });




   //insert test data
        //insertMovie("New Friends","5","Jack Black is in this","9FyZnwrJylc");
        //insertMovie("Pool Party","4","Aquabats","O6CNdlJp9c8");




        getAllMovies();

    }//end method onCreate

    public void CopyDB(InputStream inputStream,OutputStream outputStream)
            throws IOException{
        //copy 1k bytes at a time
        byte[] buffer = new byte[1024];
        int length;
        while((length = inputStream.read(buffer)) > 0)
        {
            outputStream.write(buffer,0,length);
        }
        inputStream.close();
        outputStream.close();

    }//end method CopyDB

    public void DisplayMovie(Cursor c)
    {

           Toast.makeText(this,
                "id: " + c.getString(0) + "\n" +
                        "Name: " + c.getString(1) + "\n" +
                        "Rating: " + c.getString(2)+"\n" +
                        "Description: " + c.getString(3)+"\n" +
                        "Code: "+c.getString(4),
                Toast.LENGTH_LONG).show();
    }

    public void insertMovie(String name,String rating, String desc, String code)
    {
        db.open();
        db.insertMovie(name,rating,desc,code);
        db.close();
    }



    public void getAllMovies()
    {

        db.open();
        Cursor c = db.getAllMovie();
        if(c.moveToFirst())
        {
            do{
                idList.add(c.getString(0));
                nameList.add(c.getString(1));
                ratingList.add(c.getString(2));
                descriptionList.add(c.getString(3));
                codeList.add(c.getString(4));
            }while(c.moveToNext());
        }
        db.close();
    }

    public void getMovie(int id)
    {

        db.open();
        Cursor c = db.getMovie(id);
        if(c != null) {
            c.moveToFirst();
            DisplayMovie(c);
        }
        else
            Toast.makeText(this,"No movie found",Toast.LENGTH_LONG).show();

        db.close();

    }




    class myadapter extends BaseAdapter//custom adapter to allow modification of the list view
    {

        @Override
        public int getCount() {
            return nameList.size();
        }

        @Override
        public Object getItem(int position) {
            return nameList.get(position);
        }

        @Override
        public long getItemId(int position) {
            return position;
        }

        @Override
        public View getView(int position, View convertView, ViewGroup parent) {
            LayoutInflater inflater = getLayoutInflater();
            View v = inflater.inflate(R.layout.listitem, parent, false);
            ImageView icon = (ImageView)v.findViewById(R.id.icon);

            try {
                URL url = new URL("http://img.youtube.com/vi/"+codeList.get(position)+"/default.jpg");
                Bitmap bmp = BitmapFactory.decodeStream(url.openConnection().getInputStream());
                icon.setImageBitmap(bmp);
            }
            catch(IOException e){
                System.out.println("pic not working");
            }

            TextView textView = (TextView)v.findViewById(R.id.title);
            textView.setText(nameList.get(position));
            return v;
        }
    }


}//end class