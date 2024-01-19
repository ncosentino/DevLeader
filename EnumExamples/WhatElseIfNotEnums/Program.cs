Console.WriteLine("Dev Leader Products");


public enum Products
{
    RefactoringDometrainCourse,
    SecretDometrainCourse1,
    SecretDometrainCourse2,
    SecretDometrainCourse3,
    IntroToProgrammingCourse,
    HighlightTrackerTemplate,
    DesignPatternsEbook,
    RefactoringTechniquesEbook
}

// if we're serializing enums...
// - what happens if we rename?
// - what happens if we reorder?
// - what happens if we remove?
// - what happens if we append?
// - what happens if we insert?
// - what happens if we change the value?


public enum OfferingType
{
    Course,
    Ebook,
    Book,
    Template,
    OfflineCourse,
}

public sealed record Product(
    int Id,
    OfferingType Type);

public sealed class ProductHandler(
    ResourcesHelper _resourcesHelper)
{
    public void DoStuff(
        Product product)
    {
        // indentation just to fit on screen
        if (product.Type == OfferingType.Template ||
            product.Type == OfferingType.Ebook ||
            product.Type == OfferingType.OfflineCourse)
        {
            var fileName = _resourcesHelper
                .GetDefaultDownloadFileName(
                    product.Type);
            var downloadUrl = _resourcesHelper
                .GetDownloadUrl(
                    product.Type);
        }

    }
}

public sealed class ResourcesHelper
{
    public string? GetDownloadUrl(
        OfferingType offeringType)
    {
        if (offeringType == OfferingType.Template ||
            offeringType == OfferingType.Ebook ||
            offeringType == OfferingType.OfflineCourse)
        {
            // some code to do this...
            return "TODO: get the download URL";
        }

        return null;
    }

    public string? GetDefaultDownloadFileName(
        OfferingType offeringType)
    {
        if (offeringType == OfferingType.Template ||
            offeringType == OfferingType.Ebook ||
            offeringType == OfferingType.OfflineCourse)
        {
            // some code to do this...
            return "TODO: get the file name";
        }

        return null;
    }
}





public sealed record DownloadableResource(
    int Id,
    int ProductId,
    string DownloadUrl,
    string DefaultDownloadFilename);

public sealed class ProductHandler2(
    ResourcesHelper2 _resourcesHelper)
{
    public void DoStuff(
        Product product)
    {
        var downloadableResource = _resourcesHelper
            .GetDownloadable(
                product.Id);
        // do stuff with downloadable resource
    }
}

public sealed class ResourcesHelper2
{
    public DownloadableResource? GetDownloadable(
        int productId)
    {
        // TODO: go fetch this...
    }
}








public sealed class ProductInfoHandler
{
    public string GetTitle(Products product)
    {
        switch (product)
        {
            case Products.BragDocumentTemplate:
                return "Brag Document Template";
            case Products.DesignPatternsEbook:
                return "Design Patterns Ebook";
            case Products.RefactoringDometrainCourse:
                return "Refactoring Dometrain Course";
            case Products.SecretDometrainCourse1:
                return "Secret Dometrain Course 1";
            case Products.SecretDometrainCourse2:
                return "Secret Dometrain Course 2";
            case Products.IntroToProgrammingCourse:
                return "Intro To Programming Course";
            default:
                throw new ArgumentOutOfRangeException(nameof(product), product, null);
        }
    }
}

public sealed class ProductDescriptionHandler
{
    public string GetDescription(Products product)
    {
        switch (product)
        {
            case Products.BragDocumentTemplate:
                return "A template for brag documents";
            case Products.DesignPatternsEbook:
                return "A book about design patterns";
            case Products.RefactoringDometrainCourse:
                return "A course about refactoring";
            case Products.SecretDometrainCourse1:
                return "A secret course!";
            case Products.SecretDometrainCourse2:
                return "A secret course!";
            case Products.IntroToProgrammingCourse:
                return "A course about programming";
            default:
                throw new ArgumentOutOfRangeException(nameof(product), product, null);
        }
    }
}

public sealed class PaymentHandler
{
    public double GetPrice(Products product)
    {
        switch (product)
        {
            case Products.BragDocumentTemplate:
                return 0;
            case Products.DesignPatternsEbook:
                return 4.99;
            case Products.RefactoringDometrainCourse:
                return 99.99;
            case Products.SecretDometrainCourse1:
                return 1337;
            case Products.SecretDometrainCourse2:
                return 420;
            case Products.IntroToProgrammingCourse:
                return 0;
            default:
                throw new ArgumentOutOfRangeException(nameof(product), product, null);
        }
    }
}