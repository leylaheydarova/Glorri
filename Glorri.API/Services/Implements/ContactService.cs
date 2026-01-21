using Glorri.API.DTOs.Contact;
using Glorri.API.Exceptions;
using Glorri.API.Models;
using Glorri.API.Repositories.Interfaces;
using Glorri.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Glorri.API.Services.Implements
{
    public class ContactService : IContactService
    {
        readonly IGenericRepository<Contact> _repository;

        public ContactService(IGenericRepository<Contact> repository)
        {
            _repository = repository;
        }

        public async Task<string> CreateAsync(ContactCreateDto dto)
        {
            var contact = new Contact()
            {
                Name = dto.Name,
                Url = dto.Url,
                Username = dto.Username,
                CompanyId = dto.CompanyId,
                CreatedDate = DateTime.UtcNow.AddHours(4)
            };
            await _repository.AddAsync(contact);
            await _repository.SaveAsync();
            return "Contact was created successfully!";
        }

        public async Task<List<ContactGetDto>> GetAllAsync()
        {
            var query = _repository.GetAllWhere(c => !c.IsDeleted, false);
            var dtos = await query.Select(contact => new ContactGetDto()
            {
                Id = contact.Id,
                CompanyId = contact.CompanyId,
                Name = contact.Name,
                Url = contact.Url,
                Username = contact.Username
            }).ToListAsync();
            return dtos;
        }

        public async Task<ContactGetDto> GetSingleAsync(int id)
        {
            var contact = await _repository.GetWhereAsync(c => c.Id == id && !c.IsDeleted, true);
            if (contact == null) throw new NotFoundException("contact");
            var dto = new ContactGetDto()
            {
                Id = contact.Id,
                CompanyId = contact.CompanyId,
                Name = contact.Name,
                Url = contact.Url,
                Username = contact.Username
            };
            return dto;
        }

        public async Task<string> RemoveAsync(int id)
        {
            var contact = await _repository.GetByIdAsync(id, true);
            if (contact == null) throw new NotFoundException("contact");
            _repository.Remove(contact);
            await _repository.SaveAsync();
            return "Contact was removed permanently!";
        }

        public async Task<string> ToggleAsync(int id)
        {
            var contact = await _repository.GetByIdAsync(id, true);
            if (contact == null) throw new NotFoundException("contact");
            var result = _repository.Toggle(contact);
            if (result) contact.DeletedDate = DateTime.UtcNow.AddHours(4);
            else contact.UpdatedDate = DateTime.UtcNow.AddHours(4);
            _repository.Update(contact);
            await _repository.SaveAsync();
            return result ? "Contact was removed temporarily!" : "Contact was recovered successfully!";
        }

        public async Task<string> UpdateAsync(int id, ContactUpdateDto dto)
        {
            var contact = await _repository.GetWhereAsync(c => c.Id == id && !c.IsDeleted, true);
            if (contact == null) throw new NotFoundException("contact");
            contact.Name = dto.Name != null ? dto.Name : contact.Name;
            contact.Url = dto.Url != null ? dto.Url : contact.Url;
            contact.Username = dto.Username != null ? dto.Username : contact.Username;
            contact.UpdatedDate = DateTime.UtcNow.AddHours(4);
            _repository.Update(contact);
            await _repository.SaveAsync();
            return "Contact was updated successfully!";
        }
    }
}
