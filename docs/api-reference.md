# API Reference

## Core Classes

### FarmEntity (Abstract)

Base class for all farm entities.

**Properties:**
- `Id: string` - Unique identifier
- `Name: string` - Entity name

**Methods:**
- `Produce(): Product` - Abstract method to produce products
- `GetInfo(): string` - Returns entity information
- `GetActionHistory(): List<Action>` - Returns action history

---

### Animal (Abstract)

Extends FarmEntity. Base class for all animals.

**Properties:**
- `FoodLevel: int` - Current food level (0-100)
- `Health: int` - Current health (0-100)

**Methods:**
- `Feed(int amount): void` - Feed the animal
- `MakeSound(): string` - Abstract method for animal sound
- `DecreaseFood(int amount): void` - Reduce food level

**Exceptions:**
- `InsufficientFoodException` - When feed amount ≤ 0

---

### Cow : Animal

Produces milk.

**Constructor:**
```csharp
public Cow(string name)
```

**Overridden Methods:**
- `Produce(): Product` - Returns Milk product
- `MakeSound(): string` - Returns "Moooo!"

**Production Requirements:**
- Health ≥ 30
- Food Level ≥ 20

---

### Chicken : Animal

Produces eggs.

**Constructor:**
```csharp
public Chicken(string name)
```

**Overridden Methods:**
- `Produce(): Product` - Returns Eggs product
- `MakeSound(): string` - Returns "Cluck cluck!"

**Production Requirements:**
- Health ≥ 30
- Food Level ≥ 15

---

### Sheep : Animal

Produces wool.

**Properties:**
- `WoolGrowth: int` - Current wool growth level

**Constructor:**
```csharp
public Sheep(string name)
```

**Methods:**
- `GrowWool(int amount): void` - Increase wool growth

**Overridden Methods:**
- `Produce(): Product` - Returns Wool product
- `MakeSound(): string` - Returns "Baaaa!"

**Production Requirements:**
- Wool Growth ≥ 50
- Health ≥ 30

---

### Crop (Abstract)

Extends FarmEntity. Base class for all crops.

**Properties:**
- `GrowthStage: int` - Current growth stage
- `MaturityLevel: int` - Required growth for maturity
- `IsHarvested: bool` - Whether crop has been harvested
- `IsMature: bool` - Whether crop is ready to harvest

**Methods:**
- `Grow(int days): void` - Advance crop growth
- `Harvest(): Product` - Harvest the crop

**Exceptions:**
- `CropNotMatureException` - When harvesting immature crop
- `InvalidQuantityException` - When growth days ≤ 0

---

### Wheat : Crop

Produces grain.

**Constructor:**
```csharp
public Wheat(string name) // Maturity: 7 days
```

**Production Formula:**
```csharp
Grain Quantity = GrowthStage × 5
```

---

### Corn : Crop

Produces corn cobs.

**Constructor:**
```csharp
public Corn(string name) // Maturity: 10 days
```

**Production Formula:**
```csharp
Corn Quantity = GrowthStage × 3
```

---

### VegetableCrop : Crop

Produces vegetables.

**Constructor:**
```csharp
public VegetableCrop(string name) // Maturity: 5 days
```

**Production Formula:**
```csharp
Vegetable Quantity = GrowthStage × 4
```

---

### Product (Abstract)

Base class for all farm products.

**Properties:**
- `Name: string` - Product name
- `Quantity: int` - Available quantity
- `PricePerUnit: decimal` - Price per unit

**Methods:**
- `Sell(int quantity): decimal` - Sell product, returns profit
- `SetQuantity(int value): void` - Update quantity with validation

**Product Types & Prices:**

| Product | Price per Unit |
|---------|---------------|
| Milk | K60.35 |
| Eggs | K2.95 |
| Wool | K95.00 |
| Grain | K35.00 |
| Corn Cobs | K25.72 |
| Vegetables | K45.54 |

---

### Action

Records farm activities.

**Properties:**
- `ActionType: string` - Type of action
- `Date: DateTime` - When action occurred
- `Quantity: int` - Amount involved

**Constructor:**
```csharp
public Action(string actionType, int quantity)
```

---

### FarmManager

Manages farm operations.

**Properties:**
- `FarmName: string` - Name of the farm
- `TotalProfit: decimal` - Total earnings
- `CurrentDay: int` - Current simulation day

**Methods:**
- `AddAnimal(Animal animal): void`
- `AddCrop(Crop crop): void`
- `FeedAllAnimals(int foodAmount): void`
- `FeedAnimal(string name, int amount): void`
- `ProduceFromAllAnimals(): void`
- `GrowAllCrops(int days): void`
- `HarvestCrop(string name): void`
- `HarvestAllMatureCrops(): void`
- `SellProduct(string productName, int quantity): void`
- `SimulateDay(): void`
- `DisplayReport(): void`

---

## Interfaces

### Sellable

Contract for sellable items.
```csharp
public interface Sellable
{
    decimal Sell(int quantity);
}
```

**Implemented By:**
- All Product classes

---

## Exceptions

### InsufficientFoodException
Thrown when feed amount is invalid or insufficient.

### CropNotMatureException
Thrown when attempting to harvest immature crops.

### InvalidQuantityException
Thrown when quantity values are invalid (≤ 0 or negative).

---

## Usage Examples

### Creating and Managing Animals
```csharp
// Create farm
FarmManager farm = new FarmManager("My Farm");

// Add animals
Cow bessie = new Cow("Bessie");
farm.AddAnimal(bessie);

// Feed animal
farm.FeedAnimal("Bessie", 20);

// Produce
farm.ProduceFromAllAnimals();
```

### Managing Crops
```csharp
// Plant crop
Wheat wheat = new Wheat("North Field");
farm.AddCrop(wheat);

// Grow crop
farm.GrowAllCrops(7);

// Harvest when mature
farm.HarvestCrop("North Field");
```

### Selling Products
```csharp
// Sell from inventory
farm.SellProduct("Milk", 5);
farm.SellProduct("Grain", 10);

// Check profit
Console.WriteLine($"Total Profit: ${farm.TotalProfit}");
```