namespace NorthWind.ValidationService.FluentValidation;

internal class CollectionValidationRules<T, TProperty>(
    IRuleBuilderInitialCollection<T, TProperty> ruleBuilderInitialCollection) :
    ICollectionValidationRules<T, TProperty>
{
    public ICollectionValidationRules<T, TProperty> SetValidator(
        IModelValidator<TProperty> modelValidator)
    {
        var modelValidatorBase =
            modelValidator as AbstractModelValidator<TProperty>;
        var validationService =
            modelValidatorBase?.ValidatorService as
            FluentValidationService<TProperty>;
        if (validationService != null)
        {
            ruleBuilderInitialCollection
                .SetValidator(validationService.Wrapper);
        }
        return this;
    }
}
