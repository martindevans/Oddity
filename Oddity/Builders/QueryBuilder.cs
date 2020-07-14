﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Oddity.Events;
using Oddity.Models;
using Oddity.Models.Query;
using Oddity.Models.Query.Filters;

namespace Oddity.Builders
{
    /// <summary>
    /// Represents a query builder used to retrieve data with filters specified by the user.
    /// </summary>
    /// <typeparam name="TReturn">Type which will be returned after successful API request.</typeparam>
    public class QueryBuilder<TReturn> : BuilderBase<PaginatedModel<TReturn>> where TReturn : ModelBase
    {
        private readonly string _endpoint;
        private readonly OddityCore _context;
        private readonly QueryModel _query;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryBuilder{TReturn}"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="endpoint">The endpoint used in this instance to retrieve data from API.</param>
        /// <param name="context">The Oddity context which will be used for lazy properties in models.</param>
        /// <param name="builderDelegates">The builder delegates container.</param>
        public QueryBuilder(HttpClient httpClient, string endpoint, OddityCore context, BuilderDelegates builderDelegates)
            : base(httpClient, builderDelegates)
        {
            _endpoint = endpoint;
            _context = context;
            _query = new QueryModel();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryBuilder{TReturn}"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="endpoint">The endpoint used in this instance to retrieve data from API.</param>
        /// <param name="context">The Oddity context which will be used for lazy properties in models.</param>
        /// <param name="query">The query model used to support the pagination.</param>
        /// <param name="builderDelegates">The builder delegates container.</param>
        public QueryBuilder(HttpClient httpClient, string endpoint, OddityCore context, QueryModel query, BuilderDelegates builderDelegates) 
            : base(httpClient, builderDelegates)
        {
            _endpoint = endpoint;
            _context = context;
            _query = query;
        }

        /// <inheritdoc />
        public override PaginatedModel<TReturn> Execute()
        {
            return ExecuteAsync().GetAwaiter().GetResult();
        }

        /// <inheritdoc />
        public override void Execute(PaginatedModel<TReturn> model)
        {
            ExecuteAsync(model).GetAwaiter().GetResult();
        }

        /// <inheritdoc />
        public override async Task<PaginatedModel<TReturn>> ExecuteAsync()
        {
            var content = await GetResponseFromEndpoint($"{_endpoint}", _query);
            var paginatedModel = DeserializeJson(content);
            paginatedModel.SetBuilder(this);

            foreach (var deserializedObject in paginatedModel.Data)
            {
                deserializedObject.SetContext(_context);
            }

            return paginatedModel;
        }

        /// <inheritdoc />
        public override async Task ExecuteAsync(PaginatedModel<TReturn> paginatedModel)
        {
            var content = await GetResponseFromEndpoint($"{_endpoint}", _query);
            DeserializeJson(content, paginatedModel);

            foreach (var deserializedObject in paginatedModel.Data)
            {
                deserializedObject.SetContext(_context);
            }
        }

        /// <summary>
        /// Adds a filter for the specified field which have to have an exact value.
        /// </summary>
        /// <typeparam name="T">Type of the field.</typeparam>
        /// <param name="fieldPath">Name of the field (naming convention same as in models).</param>
        /// <param name="value">Value of the field to match.</param>
        /// <returns>Builder instance.</returns>
        public QueryBuilder<TReturn> WithFieldEqual<T>(Expression<Func<TReturn, object>> selector, T value)
        {
            var fieldPath = GetPathFromExpression(selector);
            _query.Filters.Add(fieldPath, value);
            return this;
        }

        /// <summary>
        /// Adds a filter for the specified field which have to have an value greater than specified.
        /// </summary>
        /// <typeparam name="T">Type of the field.</typeparam>
        /// <param name="fieldPath">Name of the field (naming convention same as in models).</param>
        /// <param name="value">Max value of the field.</param>
        /// <returns>Builder instance.</returns>
        public QueryBuilder<TReturn> WithFieldGreaterThan<T>(Expression<Func<TReturn, object>> selector, T value)
        {
            var fieldPath = GetPathFromExpression(selector);
            _query.Filters.Add(fieldPath, new GreaterThanFilter<T>(value));
            return this;
        }

        /// <summary>
        /// Adds a filter for the specified field which have to have an value less than specified.
        /// </summary>
        /// <typeparam name="T">Type of the field.</typeparam>
        /// <param name="fieldPath">Name of the field (naming convention same as in models).</param>
        /// <param name="value">Min value of the field.</param>
        /// <returns>Builder instance.</returns>
        public QueryBuilder<TReturn> WithFieldLessThan<T>(Expression<Func<TReturn, object>> selector, T value)
        {
            var fieldPath = GetPathFromExpression(selector);
            _query.Filters.Add(fieldPath, new LessThanFilter<T>(value));
            return this;
        }

        /// <summary>
        /// Adds a filter for the specified field which have to have an value within the specified range.
        /// </summary>
        /// <typeparam name="T">Type of the field.</typeparam>
        /// <param name="fieldPath">Name of the field (naming convention same as in models).</param>
        /// <param name="from">Left side of the range.</param>
        /// <param name="to">Right side of the range.</param>
        /// <returns>Builder instance.</returns>
        public QueryBuilder<TReturn> WithFieldBetween<T>(Expression<Func<TReturn, object>> selector, T from, T to)
        {
            var fieldPath = GetPathFromExpression(selector);
            _query.Filters.Add(fieldPath, new BetweenFilter<T>(from, to));
            return this;
        }

        /// <summary>
        /// Adds a filter for the specified field which have to have an value same as one of the specified.
        /// </summary>
        /// <typeparam name="T">Type of the field.</typeparam>
        /// <param name="fieldPath">Name of the field (naming convention same as in models).</param>
        /// <param name="values">Values which have to be matched.</param>
        /// <returns>Builder instance.</returns>
        public QueryBuilder<TReturn> WithFieldIn<T>(Expression<Func<TReturn, object>> selector, params T[] values)
        {
            var fieldPath = GetPathFromExpression(selector);
            _query.Filters.Add(fieldPath, new InFilter<T>(values));
            return this;
        }

        // TODO: don't use with skip
        public QueryBuilder<TReturn> WithPage(uint page)
        {
            _query.Options.Page = page;
            return this;
        }

        public QueryBuilder<TReturn> WithLimit(uint limit)
        {
            _query.Options.Limit = limit;
            return this;
        }

        // TODO: don't use with page
        public QueryBuilder<TReturn> WithOffset(uint offset)
        {
            _query.Options.Offset = offset;
            return this;
        }

        public QueryBuilder<TReturn> SortBy(Expression<Func<TReturn, object>> selector, bool ascending = true)
        {
            var fieldPath = GetPathFromExpression(selector);
            if (_query.Options.Sort == null)
            {
                _query.Options.Sort = new Dictionary<string, SortMode>();
            }

            var sortMode = ascending ? SortMode.Ascending : SortMode.Descending;

            _query.Options.Sort[fieldPath] = sortMode;
            return this;
        }

        private string GetPathFromExpression(Expression<Func<TReturn, object>> selector)
        {
            var unaryExpression = (UnaryExpression)selector.Body;
            var memberExpression = (MemberExpression)unaryExpression.Operand;
            var members = new List<string>();

            while (memberExpression != null)
            {
                var customAttributes = memberExpression.Member.CustomAttributes;
                var jsonPropertyAttribute = customAttributes.FirstOrDefault(p => p.AttributeType == typeof(JsonPropertyAttribute));

                if (jsonPropertyAttribute != null && jsonPropertyAttribute.ConstructorArguments.Count > 0)
                {
                    members.Insert(0, (string) jsonPropertyAttribute.ConstructorArguments[0].Value);
                }
                else
                {
                    members.Insert(0, memberExpression.Member.Name);
                }

                memberExpression = memberExpression.Expression as MemberExpression;
            }

            return string.Join(".", members);
        }
    }
}