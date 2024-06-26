//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HappyPetsRescue.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Pet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pet()
        {
            this.AdoptedPet = new HashSet<AdoptedPet>();
        }
    
        public int PetID { get; set; }
        public string PetName { get; set; }
        public int PetAge { get; set; }
        public string PetStory { get; set; }
        public decimal PetWeight { get; set; }
        public string PetImage { get; set; }
        public Nullable<int> GenderID { get; set; }
        public Nullable<int> LocationID { get; set; }
        public Nullable<int> PetTypeID { get; set; }
        public Nullable<int> BreedID { get; set; }
        public Nullable<int> AdoptionStatusID { get; set; }
        public Nullable<int> UserID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AdoptedPet> AdoptedPet { get; set; }
        public virtual AdoptionStatus AdoptionStatus { get; set; }
        public virtual Breed Breed { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual Location Location { get; set; }
        public virtual PetType PetType { get; set; }
        public virtual User User { get; set; }
    }
}
