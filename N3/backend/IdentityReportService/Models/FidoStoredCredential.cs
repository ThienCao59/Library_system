using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityReportService.Models
{
    public class FidoStoredCredential
    {
        [Key]
        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public byte[] DescriptorId { get; set; } = Array.Empty<byte>();

        public byte[] PublicKey { get; set; } = Array.Empty<byte>();

        public byte[] UserHandle { get; set; } = Array.Empty<byte>();

        public uint SignatureCounter { get; set; }

        public string CredType { get; set; } = string.Empty;

        public DateTime RegDate { get; set; }

        public Guid AaGuid { get; set; }
    }
}
