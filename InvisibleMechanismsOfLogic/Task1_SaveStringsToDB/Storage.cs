namespace InvisibleMechanismsOfLogic.Task1_SaveStringsToDB
{
    public interface Storage
    {
        void Save(String data);
    
        string Retrieve(int id);
    }
}