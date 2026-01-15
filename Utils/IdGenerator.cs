public static class IdGenerator
{
    private static int _animalCounter = 0;
    private static int _cropCounter = 0;

    public static string GenerateAnimalId() => $"ANM{++_animalCounter:D4}";
    public static string GenerateCropId() => $"CRP{++_cropCounter:D4}";
}