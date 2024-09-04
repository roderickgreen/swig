// This test tests that the owner is kept alive while holding a reference to a child object

using System;
using csharp_compositionNamespace;

public class runme
{
  static void Main() 
  {
    {
      var (obj, ownerRef) = createTireFromWheel();
      gcAssertAlive(obj, ownerRef, "Wheel no longer alive 1", false);
    }

    {
      var (obj, ownerRef) = createTireFromWheelProperty();
      gcAssertAlive(obj, ownerRef, "Wheel no longer alive 2", false);
    }

    {
      var (obj, ownerRef) = createTireFromWheelRef();
      gcAssertAlive(obj, ownerRef, "Wheel no longer alive 3", false);
    }

    {
      var (obj, ownerRef) = createTireFromWheelPtr();
      gcAssertAlive(obj, ownerRef, "Wheel no longer alive 4", false);
    }

    {
      var (obj, ownerRef) = createTireFromBicycle();
      gcAssertAlive(obj, ownerRef, "Bicycle no longer alive 1", false);
    }
  }

  private static (Tire, WeakReference<Wheel>) createTireFromWheel()
  {
    var wheel = new Wheel();
    var tire = wheel.getTire();
    return (tire, new WeakReference<Wheel>(wheel));
  }

  private static (Tire, WeakReference<Wheel>) createTireFromWheelRef()
  {
    var wheel = new Wheel();
    var tire = wheel.getTireRef();
    return (tire, new WeakReference<Wheel>(wheel));
  }

  private static (Tire, WeakReference<Wheel>) createTireFromWheelPtr()
  {
    var wheel = new Wheel();
    var tire = wheel.getTirePtr();
    return (tire, new WeakReference<Wheel>(wheel));
  }

  private static (Tire, WeakReference<Wheel>) createTireFromWheelProperty()
  {
    var wheel = new Wheel();
    var tire = wheel.tire;
    return (tire, new WeakReference<Wheel>(wheel));
  }

  private static (Tire, WeakReference<Bicycle>) createTireFromBicycle()
  {
    var bike = new Bicycle();
    var wheel = bike.getFrontWheel();
    var tire = wheel.getTire();
    return (tire, new WeakReference<Bicycle>(bike));
  }


  private static void gcAssertAlive<TObj,TOwner>(TObj obj, WeakReference<TOwner> ownerRef, string description, bool shouldThrow=true) where TOwner : class
  {
    GC.Collect();
    GC.WaitForPendingFinalizers();
    GC.Collect();

    if(! ownerRef.TryGetTarget(out var owner)) {
      if(shouldThrow)
        throw new Exception(description);
      else
        Console.Error.WriteLine(description);
    }

    GC.KeepAlive(obj);
  }
}
