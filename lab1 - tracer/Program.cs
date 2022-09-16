// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

class A 
{ 
    private int a = 0;
    public partial class B
    {
        B()
        {
            A a = new A();
            a.a = 20;
        }
    }
}

public partial class B
{

}
