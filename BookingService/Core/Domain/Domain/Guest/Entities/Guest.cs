using Domain.Exceptions;
using Domain.Ports;
using Domain.ValueObjects;

namespace Domain.Entities;

public class Guest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public PersonId DocumentId { get; set; }

    private void ValidateState()
    {
        if (DocumentId == null ||
            string.IsNullOrWhiteSpace(DocumentId.IdNumber) ||
            DocumentId.IdNumber?.Length <= 3 ||
            DocumentId.DocumentType == 0)
        {
            throw new InvalidPersonDocumentIdException();
        }
        
        if(string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Surname) || string.IsNullOrWhiteSpace(Email))
        {
            throw new MissingRequiredInformationException();
        }
        
        if (!Utils.ValidateEmail(Email))
            throw new InvalidEmailException();

    }

    public void IsValid() => this.ValidateState();

    public async Task Save(IGuestRepository guestRepository) 
    {
        ValidateState();

        if(Id == 0)
        {
            Id = await guestRepository.Create(this);
        }
        else
        {
            await guestRepository.Update(this);
        }
    }
}