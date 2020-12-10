namespace Modules.Order.Converters
{
   public static class DBFlag
   {
      public static bool Accountflag { get; set; } = true;
      public static bool Productflag { get; set; } = true;

      public static void UsingMemory()
      {
         Accountflag = false;
         Productflag = false;
      }
   }
}
