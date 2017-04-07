package com.example.w0290943.assg4;

/**
 * Created by w0290943 on 11/29/2016.
 */
import android.content.*;
import android.database.*;
import android.database.sqlite.*;
import android.util.Log;

public class DbAdapter {
    public static final String KEY_ROWID = "m_id";
    public static final String KEY_NAME = "name";
    public static final String KEY_RATING ="rating";
    public static final String KEY_DESCRIPTION ="description";
    public static final String KEY_CODE ="code";
    public static final String TAG = "DBAdapter";

    private static final String DATABASE_NAME = "DB";
    private static final String DATABASE_TABLE = "movies";
    private static final int DATABASE_VERSION = 2;

    private static final String DATABASE_CREATE =
            "create table movies(m_id integer primary key autoincrement,"
                    + "name text not null,rating text not null,description text not null,code text not null);";

    private final Context context;
    private DatabaseHelper DBHelper;
    private SQLiteDatabase db;

    public DbAdapter(Context ctx)
    {
        this.context = ctx;
        DBHelper = new DatabaseHelper(context);
    }

    private static class DatabaseHelper extends SQLiteOpenHelper
    {
        DatabaseHelper(Context context)
        {
            super(context,DATABASE_NAME,null,DATABASE_VERSION);
        }

        public void onCreate(SQLiteDatabase db)
        {
            try{
                db.execSQL(DATABASE_CREATE);
            }catch(SQLException e){
                e.printStackTrace();
            }
        }//end method onCreate

        public void onUpgrade(SQLiteDatabase db,int oldVersion,int newVersion)
        {
            Log.w(TAG,"Upgrade database from version " + oldVersion + " to "
                    + newVersion + ", which will destroy all old data");
            db.execSQL("DROP TABLE IF EXISTS contacts");
            onCreate(db);
        }//end method onUpgrade
    }

    //open the database
    public DbAdapter open() throws SQLException
    {
        db = DBHelper.getWritableDatabase();
        return this;
    }

    //close the database
    public void close()
    {
        DBHelper.close();
    }

    //insert a contact into the database
    public long insertMovie(String name,String rating,String descr,String code)
    {
        ContentValues initialValues = new ContentValues();
        initialValues.put(KEY_NAME, name);
        initialValues.put(KEY_RATING, rating);
        initialValues.put(KEY_DESCRIPTION, descr);
        initialValues.put(KEY_CODE, code);
        return db.insert(DATABASE_TABLE, null, initialValues);
    }

    //delete a particular contact
    public boolean deleteMovie(long rowId)
    {
        return db.delete(DATABASE_TABLE,KEY_ROWID + "=" + rowId,null) >0;
    }

    //retrieve all the contacts
    public Cursor getAllMovie()
    {
        return db.query(DATABASE_TABLE,new String[]{KEY_ROWID,KEY_NAME,
                KEY_RATING, KEY_DESCRIPTION,KEY_CODE},null,null,null,null,null);
    }

    //retrieve a single contact
    public Cursor getMovie(long rowId) throws SQLException
    {
        Cursor mCursor = db.query(true, DATABASE_TABLE, new String[] {KEY_ROWID,
                KEY_NAME,KEY_RATING, KEY_DESCRIPTION,KEY_CODE},KEY_ROWID + "=" + rowId,null,null,null,null,null);
        if(mCursor != null)
        {
            mCursor.moveToFirst();
        }
        return mCursor;
    }

    //updates a contact
    public boolean updateMovie(long rowId,String name,String rating, String descr,String code)
    {
        ContentValues cval = new ContentValues();
        cval.put(KEY_NAME, name);
        cval.put(KEY_RATING, rating);
        cval.put(KEY_DESCRIPTION, descr);
        cval.put(KEY_CODE, code);
        return db.update(DATABASE_TABLE, cval, KEY_ROWID + "=" + rowId,null) >0;
    }

}