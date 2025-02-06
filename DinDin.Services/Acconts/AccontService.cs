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
            catch(ValidationException validationException)
            {
                throw new ValidationException(validationException.Errors);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public void Delete(int id)
        {
            try
            {
                _accontRepository.Delete(id);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public Accont GetById(int id)
        {
            return _accontRepository.GetById(id)
                ?? throw new ArgumentNullException($"Not find accont with id: {id}");
        }

        // TO DO
        //public Accont Update(Accont accont)
        //{
        //    throw new NotImplementedException();
        //}
    }
}