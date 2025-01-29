using DinDin.Domain.Acconts;
using FluentValidation;

namespace DinDin.Services.Acconts
{
    public class AccontService
    {
        private readonly IAccontRepository _accontRepository;
        private readonly IValidator<Accont> _validatorAccont;

        public AccontService(IAccontRepository accontRepository, IValidator<Accont> validatorAccont)
        {
            _accontRepository = accontRepository;
            _validatorAccont = validatorAccont;
        }

        public void Add(Accont accont)
        {
            try
            {
                _validatorAccont.ValidateAndThrow(accont);
                _accontRepository.Add(accont);
            }
            catch(FluentValidation.ValidationException validationException)
            {
                throw new FluentValidation.ValidationException(validationException.Errors);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Accont GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Accont Update(Accont accont)
        {
            throw new NotImplementedException();
        }
    }
}