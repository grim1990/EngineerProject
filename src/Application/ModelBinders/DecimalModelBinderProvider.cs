namespace Application.ModelBinders;

using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

public class DecimalModelBinderProvider : IModelBinderProvider
{
	public IModelBinder GetBinder(ModelBinderProviderContext context)
	{
		if (context.Metadata.ModelType == typeof(decimal))
		{
			return new DecimalModelBinder();
		}
		return null;
	}
}

public class DecimalModelBinder : IModelBinder
{
	public Task BindModelAsync(ModelBindingContext bindingContext)
	{
		if (bindingContext == null)
		{
			throw new ArgumentNullException(nameof(bindingContext));
		}

		var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
		if (valueProviderResult != ValueProviderResult.None)
		{
			var valueAsString = valueProviderResult.FirstValue;

			valueAsString = valueAsString.Replace(",", ".");

			if (decimal.TryParse(valueAsString, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out var result))
			{
				bindingContext.Result = ModelBindingResult.Success(result);
			}
			else
			{
				bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, "Invalid decimal number");
			}
		}

		return Task.CompletedTask;
	}
}
