public class genlist<T>{               /* "T" is the type parameter */
   private T[] data;                   /* we keep items in the array "data" */
   public int size => data.Length;     /* whenever the size is referredto, the length of the data is returned */
   //public T this[int i] => data[i];     /* indexer: we get items from our list using "variablename"[i] notation */
   public T this[int i] {get{return data[i];}set{data[i]=value;}}
   public genlist(){ data = new T[0]; }  /* constructor creates empty list */
   
   public void add(T item){              /* add item of the type "T" to the list */
        T[] newdata = new T[size+1];   /* we need a larger array (inefective but uses minimal memory) */
        System.Array.Copy(data,newdata,size); /* O(size) operations */
        newdata[size]=item;            /* add the item at the end of the list */
        data=newdata;                  /*data points to same object as newdata*/     
    }

    public void remove(int i){
        T[] newdata = new T[size-1];
        for (int j=0; j<i; j++){
            newdata[j] = data[j];
        }
        for (int j=i+1; j<size; j++){
            newdata[j-1] = data[j];
        }
        data=newdata;

    }

}