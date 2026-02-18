### 1. Как отработает программа:

```
class Animal {
    public void makeSound() {
        System.out.println("Some generic animal sound");
    }
}

class Cat extends Animal {
    // Переопределение метода makeSound
    public void makeSound() {
        System.out.println("Meow");
    }
}

public class Main {
    public static void main(String[] args) {
        Animal myCat = new Cat();
        myCat.makeSound();  // "Meow"
    }
}
```

Но если:

```
class Animal {
    // Изменен метод в суперклассе
    public void makeGenericSound() {
        System.out.println("Some generic animal sound");
    }
}
```

Ответ: 
    Программа не будет выполнена (сломается на этапе компиляции). Тут Main зависит от контракта Animal. Main известно, что 
у Animal есть метод makeSound, но как только его имя сменится, то контракт будет нарушен. При этом Cat также зависит от Animal,
но нарушения не произойдёт, поскольку Cat наследует методы Animal.

### 2. Как отработает программа:

```
class Animal {
    public void makeSound() {
        System.out.println("Some generic animal sound");
    }
}

class Cat extends Animal {
    @Override
    public void makeSound(int numberOfSounds) {
        for (int i = 0; i < numberOfSounds; i++) {
            System.out.println("Meow");
        }
    }
    
    @Override
    public void makeSound() {
        System.out.println("Meow");
    }
}

public class Main {
    public static void main(String[] args) {
        Animal cat = new Cat();
        cat.makeSound();
        cat.makeSound(3);
    }
}
```

Ответ:
    Программа не будет выполнена (сломается на этапе компиляции). Несмотря на то, что создаётся тип Cat, Main зависит от 
контракта Animal. Поскольку в Animal не определено перегрузки метода makeSound которая принимает целочисленный параметр,
то программа не сможет быть выполнена. Также нарушается наследование Cat от Animal - Cat пытается переопределить метод, 
которого нет в базовом классе.

### 3. Какие незримые механизмы логики могут проявиться тут?

```
/*
<dependency>
    <groupId>com.fasterxml.jackson.core</groupId>
    <artifactId>jackson-databind</artifactId>
    <version>2.9.10</version>
</dependency>
<dependency>
    <groupId>com.fasterxml.jackson.core</groupId>
    <artifactId>jackson-databind</artifactId>
    <version>2.12.5</version>
</dependency>
*/

import com.fasterxml.jackson.databind.ObjectMapper;

import java.io.IOException;
import java.util.HashMap;
import java.util.Map;

public class Main {
    public static void main(String[] args) {
        // Создаем объект ObjectMapper для парсинга JSON
        ObjectMapper objectMapper = new ObjectMapper();

        String jsonString = "{\"name\":\"John\", \"age\":30}";

        try {
            // Парсим JSON-строку в HashMap
            Map<String, Object> result = objectMapper.readValue(jsonString, HashMap.class);

            System.out.println("Name: " + result.get("name"));
        } catch (IOException e) {
            // Обработка ошибки парсинга
            e.printStackTrace();
        }

        try {
            String prettyJson = objectMapper.writerWithDefaultPrettyPrinter().writeValueAsString(result);
            System.out.println("Pretty JSON: " + prettyJson);
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}
```

Ответ:
1. Указана зависимость от двух разных версий одного пакета "jackson-databind" - лучше явно указывать нужную версию, а не 
   полагаться на автоматическую систему разрешения зависимостей. Её работа неявная.
2. Переменная result используется за пределами области видимости - объявлен и инициализирован в первом try..catch, а используется во 
   втором. 
3. Нет полной уверенности, но подозреваю что может смениться форматирование и порядок данных при преобразовании строки json
   в HashMap и обратно. Например, в .NET словарь не гарантирует порядок.