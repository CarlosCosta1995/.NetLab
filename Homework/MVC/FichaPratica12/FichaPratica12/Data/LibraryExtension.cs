using FichaPratica12.Models;

namespace FichaPratica12.Data
{
    public static class LibraryExtension // 5.d
    {
        public static void CreateDbIfNotExists(this IHost host)
        {
            using(var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<LibraryContext>();

                //Create Db if not exists
                if (context.Database.EnsureCreated())
                {
                    LibraryDBInitializer.InsertData(context);
                }
            }
        }
    }
}
