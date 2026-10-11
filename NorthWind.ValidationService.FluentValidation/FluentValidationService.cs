namespace NorthWind.ValidationService.FluentValidation;

internal class FluentValidationService<T> : IValidationService<T>
{
    internal readonly AbstractValidatorImplementation<T> Wrapper =
        new AbstractValidatorImplementation<T>();

    public IValidationRules<T, TProperty> AddRuleFor<TProperty>(
        Expression<Func<T, TProperty>> expression) =>
        new ValidationRules<T, TProperty>(Wrapper.RuleFor(expression));

    public ICollectionValidationRules<T, TProperty> AddRuleForEach<TProperty>(
        Expression<Func<T, IEnumerable<TProperty>>> expression) =>
        new CollectionValidationRules<T, TProperty>(
            Wrapper.RuleForEach(expression));

    public async Task<IEnumerable<ValidationError>> Validate(T model)
    {
        var result = await Wrapper.ValidateAsync(model);
        IEnumerable<ValidationError> errors = [];
        if (!result.IsValid)
        {
            errors = result.Errors
                .Select(e => new ValidationError(
                    e.PropertyName, e.ErrorMessage));
        }
        return errors;
    }
}
