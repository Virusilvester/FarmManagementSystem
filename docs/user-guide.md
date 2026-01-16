# User Guide

## Getting Started

### Installation

1. Clone the repository:
```bash
git clone https://github.com/Virusilvester/FarmManagementSystem.git
```

2. Navigate to the project directory:
```bash
cd FarmManagementSystem
```

3. Compile and run:
```bash
dotnet run
# or open in Visual Studio
```

## Using the System

### Main Menu

When you start the program, you'll see a menu with 12 options:

1. **Add Animal** - Add a new cow, chicken, or sheep
2. **Add Crop** - Plant wheat, corn, or vegetables
3. **Feed Animal** - Feed a specific animal
4. **Feed All Animals** - Feed all animals at once
5. **Produce from All Animals** - Collect products from animals
6. **Grow All Crops** - Advance crop growth
7. **Harvest Crop** - Harvest a specific crop
8. **Harvest All Mature Crops** - Harvest all ready crops
9. **Sell Product** - Sell products from inventory
10. **Simulate Day** - Advance time by one day
11. **Display Farm Report** - View complete farm status
12. **Exit** - Close the program

### Tutorial: Your First Day

#### Step 1: Add Animals
Choose option 1
Select: 1 (Cow)
Name: Chub

#### Step 2: Add Crops
Choose option 2
Select: 1 (Wheat)
Name: North Field

#### Step 3: Feed Your Animals
Choose option 4
Enter: 20 (food amount)

#### Step 4: Grow Your Crops
Choose option 6
Enter: 1 (days)

#### Step 5: Check Your Farm
Choose option 11
View the complete report!

### Advanced Features

#### Day Simulation
Use option 10 to simulate the passage of time:
- Animals lose food (10 units per day)
- Sheep grow wool (5 units per day)
- Day counter advances

#### Product Management
1. Produce products from healthy animals
2. Harvest mature crops
3. Sell products for profit
4. Track total earnings

### Tips & Tricks

- **Keep animals fed**: Animals below 20 food lose health
- **Water your crops**: Grow crops daily until mature
- **Harvest timing**: Wheat (7 days), Corn (10 days), Vegetables (5 days)
- **Sheep wool**: Requires 50+ wool growth to shear
- **Health matters**: Unhealthy animals can't produce

## Troubleshooting

### Common Errors

**"Animal is too unhealthy to produce"**
- Solution: Feed the animal to restore health

**"Crop is not mature yet"**
- Solution: Continue growing until maturity level reached

**"Cannot sell X. Only Y available"**
- Solution: Check inventory, reduce sell quantity

**"Feed amount must be greater than 0"**
- Solution: Enter a positive number

## Example Workflow
Day 1:

Add 2 cows, 3 chickens, 1 sheep
Plant 2 wheat fields, 1 corn field
Feed all animals (30 units)
Grow all crops (1 day)

Day 2-7:

Simulate day
Feed animals
Grow crops
Produce from animals

Day 7:

Harvest wheat (mature)
Sell products
Check total profit

Day 10:

Harvest corn
Continue operations
