
namespace FPS_Homework_Framework
{

    // a base class for managers
    public class BaseManager<T> where T : new()
    {
        public static T Instance
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = new T();
                }
                return mInstance;
            }
        }

        private static T mInstance = default(T);
        
};

    
        


}
