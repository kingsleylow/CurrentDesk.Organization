using System;
using System.Linq;
using System.Collections.Generic;
using CurrentDesk.DAL;
using CurrentDesk.Models;

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template at 15-07-13 10:25:32 AM
//	   and will be overwritten as soon as the template is executed

//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CurrentDesk.Repository.CurrentDesk
{   
	internal partial class L_AccountActivityTypeRepository
	{
		private IRepository<L_AccountActivityType> _repository {get;set;}
		public IRepository<L_AccountActivityType> Repository
		{
			get { return _repository; }
			set { _repository = value; }
		}
		
		public L_AccountActivityTypeRepository(IRepository<L_AccountActivityType> repository, IUnitOfWork unitOfWork)
    	{
    		Repository = repository;
			Repository.UnitOfWork = unitOfWork;
    	}
		
		public IQueryable<L_AccountActivityType> All()
		{
			return Repository.All();
		}

		public void Add(L_AccountActivityType entity)
		{
			Repository.Add(entity);
		}
		
		public void Delete(L_AccountActivityType entity)
		{
			Repository.Delete(entity);
		}

		public void Save()
		{
			Repository.Save();
		}
	}
}