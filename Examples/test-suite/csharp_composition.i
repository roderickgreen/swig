%module csharp_composition

%inline %{
class Tire {
  public:
    int width;
  public:
    Tire() : width(42) {}
    int getWidth() { return width; }
};

class Wheel {
  public:
    Tire tire;  
  public:
    Tire getTire() { return tire; }
    Tire& getTireRef() { return tire; }
    Tire* getTirePtr() { return &tire; }
};

class Bicycle {
  public:
    Wheel frontWheel;
    Wheel backWheel;  
  public:
    Wheel getFrontWheel() { return frontWheel; }
    Wheel getBackWheel() { return backWheel; }

    Wheel& getFrontWheelRef() { return frontWheel; }
    Wheel& getBackWheelRef() { return backWheel; } 

    Wheel* getFrontWheelPtr() { return &frontWheel; }
    Wheel* getBackWheelPtr() { return &backWheel; }    
};
%}
