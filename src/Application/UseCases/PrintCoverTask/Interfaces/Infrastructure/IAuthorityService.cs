namespace PrintCoverGenerator.Interfaces.Infrastructure;

public interface IAuthorityService
{
    Task PermitReadToPublic(string presentationId);
    Task DenyPublicAccess(string presentationId);
}