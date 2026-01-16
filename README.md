classDiagram
    direction TB

    %% =========================
    %% Base Abstraction
    %% =========================
    class FarmEntity {
        <<abstract>>
        -string id
        -string name
        -List~Action~ actions
        +Produce()
        +GetInfo()
    }

    %% =========================
    %% Composition
    %% =========================
    FarmEntity "1" *-- "many" Action : composition

    class Action {
        -string type
        -DateTime date
        -int qty
    }

    %% =========================
    %% Inheritance: Animal & Crop
    %% =========================
    FarmEntity <|-- Animal
    FarmEntity <|-- Crop

    class Animal {
        -int food
        -int health
        +Feed()
        +Sound()
    }

    class Crop {
        -int growth
        -bool mature
        +Grow()
        +Harvest()
    }

    %% =========================
    %% Concrete Implementations
    %% =========================
    Animal <|-- Cow
    Animal <|-- Chicken
    Animal <|-- Sheep

    Crop <|-- Wheat
    Crop <|-- Corn
    Crop <|-- Vegetables

    %% =========================
    %% Product & Selling
    %% =========================
    FarmEntity --> Product : produces

    class Product {
        -string name
        -int qty
        -decimal price
        +Sell()
    }

    %% =========================
    %% Interface
    %% =========================
    class Sellable {
        <<interface>>
        +Sell()
    }

    Product ..|> Sellable

    %% =========================
    %% Product Inheritance
    %% =========================
    Product <|-- Milk
    Product <|-- Eggs
    Product <|-- Wool
    Product <|-- Grain
    Product <|-- CornCobs
    Product <|-- Vegs
