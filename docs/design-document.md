# Farm Management System - OOP Design Documentation

## Table of Contents
1. [System Overview](#system-overview)
2. [Class Hierarchy](#class-hierarchy)
3. [OOP Principles Applied](#oop-principles-applied)
4. [Design Decisions](#design-decisions)
5. [Class Relationships](#class-relationships)

---

## System Overview

The Farm Management System is a comprehensive application that simulates farm operations including animal care, crop cultivation, production, and sales management. The system is built using object-oriented programming principles to create a maintainable, extensible, and robust solution.

**Core Functionality:**
- Manage animals (Cows, Chickens, Sheep)
- Cultivate crops (Wheat, Corn, Vegetables)
- Produce and sell farm products
- Track all farm activities and transactions
- Simulate time progression

---

## Class Hierarchy

```
FarmEntity (Abstract Base Class)
│
├── Animal (Abstract)
│   ├── Cow
│   ├── Chicken
│   └── Sheep
│
└── Crop (Abstract)
    ├── Wheat
    ├── Corn
    └── VegetableCrop

Product (Abstract)
├── Milk
├── Eggs
├── Wool
├── Grain
├── CornCobs
└── Vegetables

Interfaces:
- Sellable

Supporting Classes:
- Action (Composition)
- FarmManager (Aggregation)

Custom Exceptions:
- InsufficientFoodException
- CropNotMatureException
- InvalidQuantityException
```

---

## OOP Principles Applied

### 1. **Abstraction**

**FarmEntity Abstract Class:**
- Provides common interface for all farm entities
- Defines abstract method `produce()` that must be implemented by all subclasses
- Encapsulates shared properties: `id`, `name`, `actionHistory`

**Animal Abstract Class:**
- Abstracts common animal behaviors: `feed()`, `makeSound()`
- Defines shared state: `foodLevel`, `health`
- Forces subclasses to implement `produce()` and `makeSound()`

**Crop Abstract Class:**
- Abstracts crop lifecycle: `grow()`, `harvest()`
- Manages growth stages and maturity
- Enforces harvest restrictions

**Benefits:**
- Hides complex implementation details
- Provides clear contracts for subclasses
- Enables working with entities at different abstraction levels

### 2. **Encapsulation**

**Private Fields with Public Properties:**
```csharp
private string name;
public string Name => name;
```

**All fields are private**, accessed only through:
- **Getters**: Read-only properties (e.g., `Name`, `Id`, `Health`)
- **Setters**: Validated methods (e.g., `SetName()`, `SetQuantity()`)

**Validation Examples:**
- Feed amount must be positive
- Crop growth days must be positive
- Products cannot have negative quantities
- Names cannot be empty

**Benefits:**
- Data integrity through validation
- Controlled access to internal state
- Protection against invalid operations
- Ability to change implementation without breaking external code

### 3. **Inheritance**

**Multi-Level Hierarchy:**

```
FarmEntity
    ↓
  Animal → Cow, Chicken, Sheep
    ↓
  Crop → Wheat, Corn, VegetableCrop
```

**What's Inherited:**
- **From FarmEntity**: ID generation, name management, action tracking
- **From Animal**: Food/health management, feeding mechanism
- **From Crop**: Growth tracking, maturity checking

**Method Overriding:**
- Each animal overrides `produce()` with specific product
- Each animal overrides `makeSound()` with unique sound
- Each crop overrides `produce()` with specific yield calculation

**Benefits:**
- Code reuse (DRY principle)
- Consistent interface across related classes
- Easy to add new animal/crop types
- Shared behavior inherited automatically

### 4. **Polymorphism**

**Runtime Polymorphism:**
```csharp
FarmEntity entity = new Cow("Bessie");
Product product = entity.Produce(); // Calls Cow's implementation
```

**Method Overriding Examples:**

| Class | produce() Returns | makeSound() Returns |
|-------|------------------|---------------------|
| Cow | Milk | "Moooo!" |
| Chicken | Eggs | "Cluck cluck!" |
| Sheep | Wool | "Baaaa!" |
| Wheat | Grain | N/A |
| Corn | CornCobs | N/A |

**Interface Polymorphism:**
```csharp
Sellable product = new Milk(10);
decimal profit = product.Sell(5); // Works with any Sellable
```

**Benefits:**
- Single interface, multiple implementations
- Process different entities uniformly
- Easy to extend with new types
- Clean, maintainable code in FarmManager

### 5. **Composition**

**Action Class:**
Each `FarmEntity` **HAS-A** list of `Action` objects:
```csharp
private List<Action> actionHistory;
```

**Relationship Type:** Composition (strong ownership)
- Actions cannot exist without their parent entity
- Actions are created and destroyed with the entity
- Complete lifecycle dependency

**Tracked Actions:**
- Feed, Produce, Grow, Harvest, Sell

**Benefits:**
- Complete audit trail of farm operations
- Historical data for analysis
- Separation of concerns (actions separate from entities)

### 6. **Interfaces**

**Sellable Interface:**
```csharp
public interface Sellable
{
    decimal Sell(int quantity);
}
```

**Implementation:**
All `Product` classes implement `Sellable`, providing consistent selling behavior.

**Benefits:**
- Contract enforcement (all products must be sellable)
- Multiple inheritance alternative
- Uniform selling mechanism across products
- Easy to add new sellable items

---

## Design Decisions

### 1. **Static ID Generation**

**Decision:** Use static counter in `FarmEntity` for automatic ID generation.

```csharp
private static int nextId = 1;
private string id;

protected FarmEntity(string name)
{
    this.id = "FE" + (nextId++);
}
```

**Rationale:**
- Ensures unique IDs automatically
- No external ID management needed
- Simple and efficient
- Sequential numbering for easy tracking

**Trade-off:** IDs are session-specific (reset on restart)

### 2. **Separate Product Hierarchy**

**Decision:** Products are separate from entities (not inherited).

**Rationale:**
- A cow IS-NOT milk (wrong relationship)
- A cow PRODUCES milk (correct relationship)
- Products have different lifecycle than entities
- Products can be stored, sold, transferred independently

**Structure:**
```
Animal/Crop → produces → Product → implements Sellable
```

### 3. **Action History via Composition**

**Decision:** Use composition instead of inheritance for action tracking.

**Rationale:**
- Actions are separate concerns from entity behavior
- Flexible: can change action tracking without affecting entities
- Reusable: same Action class for all entities
- Single Responsibility Principle: entities manage state, actions record history

### 4. **Exception Handling Strategy**

**Decision:** Create domain-specific custom exceptions.

**Custom Exceptions:**
- `InsufficientFoodException` - Animal feeding issues
- `CropNotMatureException` - Premature harvesting
- `InvalidQuantityException` - Invalid amounts/quantities

**Rationale:**
- Clear, meaningful error messages
- Easier to catch and handle specific problems
- Better debugging and user feedback
- Domain language in error handling

### 5. **FarmManager as Facade**

**Decision:** Create `FarmManager` class to coordinate all operations.

**Responsibilities:**
- Aggregate all animals and crops
- Coordinate batch operations
- Manage inventory and profit
- Simulate time progression

**Design Pattern:** Facade Pattern

**Rationale:**
- Simplifies complex subsystem interactions
- Single point of control
- Cleaner client code (Program.Main)
- Encapsulates farm-level business logic

### 6. **Validation at Multiple Levels**

**Decision:** Validate inputs at every entry point.

**Where Validation Occurs:**
- Property setters (name, quantity)
- Method parameters (feed amount, growth days)
- Business logic (maturity checks, health requirements)

**Rationale:**
- Fail fast principle
- Data integrity at all times
- Clear error messages at point of failure
- Defense in depth

### 7. **Immutable Action History**

**Decision:** Actions cannot be modified after creation.

```csharp
public Action(string actionType, int quantity)
{
    this.actionType = actionType;
    this.date = DateTime.Now; // Set once
    this.quantity = quantity;
}
```

**Rationale:**
- Historical data should not change
- Audit trail integrity
- Prevents tampering with records
- Reflects real-world immutability of past events

---

## Class Relationships

### 1. **Inheritance Relationships (IS-A)**

- Cow **IS-A** Animal
- Chicken **IS-A** Animal
- Sheep **IS-A** Animal
- Wheat **IS-A** Crop
- Corn **IS-A** Crop
- VegetableCrop **IS-A** Crop
- Animal **IS-A** FarmEntity
- Crop **IS-A** FarmEntity
- Milk **IS-A** Product

### 2. **Composition Relationships (HAS-A, Strong)**

- FarmEntity **HAS-A** List<Action>
  - Actions owned by entity
  - Created and destroyed together

### 3. **Aggregation Relationships (HAS-A, Weak)**

- FarmManager **HAS-A** List<Animal>
- FarmManager **HAS-A** List<Crop>
- FarmManager **HAS-A** List<Product>
  - Manager coordinates entities
  - Entities can exist independently

### 4. **Interface Implementation (CAN-DO)**

- All Products **CAN-DO** Sellable
  - Guarantees selling capability

### 5. **Dependency Relationships**

- Animal methods **DEPEND-ON** Product classes
- Crop methods **DEPEND-ON** Product classes
- FarmManager **DEPENDS-ON** all entity types

### 6. **Association Relationships**

- Animal **PRODUCES** Product
- Crop **PRODUCES** Product
- Product **RECORDS** Transaction in Action

---

## UML Diagram Summary

```
┌─────────────────┐
│   FarmEntity    │ (Abstract)
├─────────────────┤
│ -id: string     │
│ -name: string   │
│ -actions: List  │◆────────┐
├─────────────────┤         │
│ +Produce()      │         │ Composition
│ +GetInfo()      │         │
└────────┬────────┘         │
         │                  │
    Inheritance          ┌──▼──────┐
         │               │ Action  │
    ┌────┴────┐          ├─────────┤
    │         │          │ -type   │
┌───▼───┐ ┌──▼────┐     │ -date   │
│Animal │ │ Crop  │     │ -qty    │
├───────┤ ├───────┤     └─────────┘
│ -food │ │ -growth│
│ -health│ │ -mature│
├───────┤ ├────────┤
│Feed() │ │Grow()  │
│Sound()│ │Harvest()│
└───┬───┘ └───┬────┘
    │         │
    │         │ Inheritance
    │         │
┌───┴──────────┴────┐
│ Cow, Chicken,     │
│ Sheep, Wheat,     │
│ Corn, Vegetables  │
└───────────────────┘
         │
         │ Produces
         ▼
    ┌─────────┐
    │ Product │◇─────── Sellable
    ├─────────┤        (Interface)
    │ -name   │
    │ -qty    │
    │ -price  │
    ├─────────┤
    │ Sell()  │
    └────┬────┘
         │
    Inheritance
         │
┌────────┴────────┐
│ Milk, Eggs,     │
│ Wool, Grain,    │
│ CornCobs, Vegs  │
└─────────────────┘
```

---

## Extensibility

The system is designed for easy extension:

### Adding New Animals:
1. Create class extending `Animal`
2. Override `Produce()` and `MakeSound()`
3. Add to menu in Program

### Adding New Crops:
1. Create class extending `Crop`
2. Override `Produce()` with yield formula
3. Set maturity level in constructor

### Adding New Products:
1. Create class extending `Product`
2. Automatically implements `Sellable`
3. Set name, quantity, price in constructor

### Adding New Actions:
1. Simply call `AddAction(type, quantity)`
2. Automatically tracked in history

---

## Conclusion

This Farm Management System demonstrates comprehensive use of OOP principles:

- ✅ **Abstraction**: Clear separation of interface and implementation
- ✅ **Encapsulation**: Data protection with validated access
- ✅ **Inheritance**: Code reuse through class hierarchies
- ✅ **Polymorphism**: Flexible, extensible entity processing
- ✅ **Composition**: Modular action tracking
- ✅ **Interfaces**: Contract-based design for sellable items

The design prioritizes **maintainability**, **extensibility**, and **robustness** while providing a realistic simulation of farm operations. The system can easily scale to accommodate new entity types, products, and features without major refactoring.