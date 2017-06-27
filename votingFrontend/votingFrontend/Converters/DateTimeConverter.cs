// <copyright file="DateTimeConverter.cs" company="UCOL 3rd Year Bachelor of Information and Communication Assignment">
// Copyright (c) UCOL 3rd Year Bachelor of Information and Communication Assignment. All rights reserved.
// </copyright>

namespace VotingFrontend.Converters
{
    using System;
    using Windows.UI.Xaml.Data;

    /// <summary>
    /// Converts a dateTime object into a dateTimeOffset object and back
    /// </summary>
    public class DateTimeConverter : IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            try
            {
                DateTime date = (DateTime)value;
                return new DateTimeOffset(date);
            }
            catch
            {
                return DateTimeOffset.MinValue;
            }
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            try
            {
                DateTimeOffset dto = (DateTimeOffset)value;
                return dto.DateTime;
            }
            catch
            {
                return DateTime.MinValue;
            }
        }
    }
}
