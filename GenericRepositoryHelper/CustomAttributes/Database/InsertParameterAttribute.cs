﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericRepositoryHelper.CustomAttributes.Database
{
    // Este atributo permitira asignar las propiedades que se utilizaran de la clase para 
    // insertar el registro en la base de datos por medio a un storedprocedure
    // Se necesita el tipo de dato de sql
    // el nombre del parametro (en caso de ser diferente al nombre de la propiedad,
    // y el tipo de parametro (ya se input, output o input/output)
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class InsertParameterAttribute : Attribute
    {
        private readonly string databaseName;
        private readonly ParameterDirection parameterDirection;
        private readonly SqlDbType dataType;
        private readonly int? size;

        public InsertParameterAttribute(SqlDbType dataType, string databaseName = null, ParameterDirection parameterDirection = ParameterDirection.Input, int size = 0)
        {
            this.databaseName = databaseName;
            this.parameterDirection = parameterDirection;
            this.dataType = dataType;
            if (size > 0)
                this.size = size;
            else
                this.size = null;
        }
        public bool HasDifferentDatabaseName { get => databaseName != null; }
        public string DatabaseName { get => databaseName; }
        public ParameterDirection ParameterDirection { get => parameterDirection; }
        public bool IsParameterDirectionOutput{ get => parameterDirection == ParameterDirection.Output; }
        public string ParameterName { get => databaseName != null ? $"@{databaseName}" : null; }
        public SqlDbType DataType { get => dataType; }
        public int? Size { get => size; }
        public bool IsIdAttribute { get => HasDifferentDatabaseName && DatabaseName.Trim().ToLower() == "id"; }
    }
}
