

namespace BlApi;

public interface IBl//ממשק ראשי שירכז את כל הממשקים של השכבה
{
    public IProduct Product { get; }
    public ICart Cart { get; }
    public IOrder Order { get; }

}

