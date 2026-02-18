### Как отработает программа в таком виде, и если убрать аннотацию @Override метода makeSound() класса Cat.

```
class Animal {
    public void makeSound() {
        System.out.println("Some generic animal sound");
    }
}

class Cat extends Animal {

    @Override
    public void makeSound() {
        System.out.println("Meow");
    }
}

public class Main {
    public static void main(String[] args) {
        Animal cat = new Cat();
        cat.makeSound();
    }
}
```

Ответ:
    Программа отработает одинаково в обоих случаях - будет вызов метода makeSound класса Cat с записью строки "Meow".
