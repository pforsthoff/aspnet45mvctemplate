using System;
using System.Collections.Generic;
using System.Text;

namespace DataDictionary.Business.Managers
{
    public class ManagerBase
    {
        //private readonly IValidator _validator;
        //private UserPrinciple _currentPrinciple;
        //private readonly ILogger _log;

        //protected IValidator Validator
        //{
        //    get { return _validator; }
        //}

        //protected ILogger Log
        //{
        //    get { return _log; }
        //}

        //protected virtual UserPrinciple CurrentPrincipal
        //{
        //    get
        //    {
        //        if (_currentPrinciple != null)
        //            return _currentPrinciple;

        //        _currentPrinciple = Thread.CurrentPrincipal as UserPrinciple;
        //        if (_currentPrinciple == null)
        //            _currentPrinciple = new UserPrinciple(Thread.CurrentPrincipal.Identity, null);

        //        return _currentPrinciple;
        //    }
        //}

        //public ManagerBase(IValidator validator, ILogger log)
        //{
        //    _validator = validator;
        //    _log = log;
        //}

        //protected virtual void EnsureIsValid<T>(T entity)
        //{
        //    var validationResults = Validator.Validate<T>(entity);
        //    if (validationResults != null && !validationResults.IsValid)
        //        throw new ValidationException(validationResults);
        //}

        //protected void ThrowValidationException(string key, string message)
        //{
        //    var results = new ValidationResults();
        //    results.AddResult(new ValidationResult(key ?? "", message));

        //    throw new ValidationException(results);
        //}
    }
}
