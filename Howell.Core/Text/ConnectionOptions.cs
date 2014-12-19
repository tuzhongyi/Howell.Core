using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using System.Collections;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Howell.Text
{
    internal class ConnectionOptions
    {
        // Fields
        private readonly Hashtable _parsetable;
        private PermissionSet _permissionset;
        private readonly string _usersConnectionString;
        private const string ConnectionStringQuoteOdbcValuePattern = "^\\{([^\\}\0]|\\}\\})*\\}$";
        private static readonly Regex ConnectionStringQuoteOdbcValueRegex = new Regex("^\\{([^\\}\0]|\\}\\})*\\}$", RegexOptions.Compiled | RegexOptions.ExplicitCapture);
        private const string ConnectionStringQuoteValuePattern = "^[^\"'=;\\s\\p{Cc}]*$";
        private static readonly Regex ConnectionStringQuoteValueRegex = new Regex("^[^\"'=;\\s\\p{Cc}]*$", RegexOptions.Compiled);
        private const string ConnectionStringValidKeyPattern = @"^(?![;\s])[^\p{Cc}]+(?<!\s)$";
        private static readonly Regex ConnectionStringValidKeyRegex = new Regex(@"^(?![;\s])[^\p{Cc}]+(?<!\s)$", RegexOptions.Compiled);
        private const string ConnectionStringValidValuePattern = "^[^\0]*$";
        private static readonly Regex ConnectionStringValidValueRegex = new Regex("^[^\0]*$", RegexOptions.Compiled);
        internal const string DataDirectory = "|datadirectory|";
        internal readonly bool HasPasswordKeyword;
        internal readonly NameValuePair KeyChain;

        // Methods
        protected ConnectionOptions(ConnectionOptions connectionOptions)
        {
            this._usersConnectionString = connectionOptions._usersConnectionString;
            this.HasPasswordKeyword = connectionOptions.HasPasswordKeyword;
            this._parsetable = connectionOptions._parsetable;
            this.KeyChain = connectionOptions.KeyChain;
        }

        public ConnectionOptions(string connectionString)
            : this(connectionString, null)
        {
        }

        public ConnectionOptions(string connectionString, Hashtable synonyms)
        {
            this._parsetable = new Hashtable();
            this._usersConnectionString = (connectionString != null) ? connectionString : "";
            if (0 < this._usersConnectionString.Length)
            {
                this.KeyChain = ParseInternal(this._parsetable, this._usersConnectionString, true, synonyms, false);
                this.HasPasswordKeyword = this._parsetable.ContainsKey("password") || this._parsetable.ContainsKey("pwd");
            }
        }

        internal static void AppendKeyValuePairBuilder(StringBuilder builder, string keyName, string keyValue)
        {
            if (builder == null)
                throw new ArgumentNullException("builder");
            if (keyName == null)
                throw new ArgumentNullException("keyName");
            if (keyName.Length == 0)
                throw new ArgumentOutOfRangeException("keyName");
            if ((keyName == null) || !ConnectionStringValidKeyRegex.IsMatch(keyName))
            {
                throw new ArgumentException(String.Format("KeyName {0} is invalid.", keyName));
            }
            if ((keyValue != null) && !IsValueValidInternal(keyValue))
            {
                throw new ArgumentException(String.Format("KeyValue {0} is invalid.", keyValue));
            }
            if ((0 < builder.Length) && (';' != builder[builder.Length - 1]))
            {
                builder.Append(";");
            }
            builder.Append(keyName.Replace("=", "=="));
            builder.Append("=");
            if (keyValue != null)
            {
                if (ConnectionStringQuoteValueRegex.IsMatch(keyValue))
                {
                    builder.Append(keyValue);
                }
                else if ((-1 != keyValue.IndexOf('"')) && (-1 == keyValue.IndexOf('\'')))
                {
                    builder.Append('\'');
                    builder.Append(keyValue);
                    builder.Append('\'');
                }
                else
                {
                    builder.Append('"');
                    builder.Append(keyValue.Replace("\"", "\"\""));
                    builder.Append('"');
                }
            }
        }

        private static bool CompareInsensitiveInvariant(string strvalue, string strconst)
        {
            return (0 == StringComparer.OrdinalIgnoreCase.Compare(strvalue, strconst));
        }

        public bool ContainsKey(string keyword)
        {
            return this._parsetable.ContainsKey(keyword);
        }

        internal static int ConvertToInt32Internal(string keyName, string stringValue)
        {
            int num;
            try
            {
                num = int.Parse(stringValue, NumberStyles.Integer, CultureInfo.InvariantCulture);
            }
            catch (FormatException exception2)
            {
                throw new ArgumentException(String.Format("KeyName {0} value {1} is an invalid connection option value.", keyName, stringValue), exception2);
            }
            catch (OverflowException exception)
            {
                throw new ArgumentException(String.Format("KeyName {0} value {1} is an invalid connection option value.", keyName, stringValue), exception);
            }
            return num;
        }

        public bool ConvertValueToBoolean(string keyName, bool defaultValue)
        {
            object obj2 = this._parsetable[keyName];
            if (obj2 == null)
            {
                return defaultValue;
            }
            return ConvertValueToBooleanInternal(keyName, (string)obj2);
        }

        internal static bool ConvertValueToBooleanInternal(string keyName, string stringValue)
        {
            if (CompareInsensitiveInvariant(stringValue, "true") || CompareInsensitiveInvariant(stringValue, "yes"))
            {
                return true;
            }
            if (!CompareInsensitiveInvariant(stringValue, "false") && !CompareInsensitiveInvariant(stringValue, "no"))
            {
                string strvalue = stringValue.Trim();
                if (CompareInsensitiveInvariant(strvalue, "true") || CompareInsensitiveInvariant(strvalue, "yes"))
                {
                    return true;
                }
                if (!CompareInsensitiveInvariant(strvalue, "false") && !CompareInsensitiveInvariant(strvalue, "no"))
                {
                    throw new ArgumentException(String.Format("KeyName {0} value {1} is an invalid connection option value.", keyName, stringValue));
                }
            }
            return false;
        }

        public int ConvertValueToInt32(string keyName, int defaultValue)
        {
            object obj2 = this._parsetable[keyName];
            if (obj2 == null)
            {
                return defaultValue;
            }
            return ConvertToInt32Internal(keyName, (string)obj2);
        }

        public bool ConvertValueToIntegratedSecurity()
        {
            object obj2 = this._parsetable["integrated security"];
            if (obj2 == null)
            {
                return false;
            }
            return this.ConvertValueToIntegratedSecurityInternal((string)obj2);
        }

        internal bool ConvertValueToIntegratedSecurityInternal(string stringValue)
        {
            if ((CompareInsensitiveInvariant(stringValue, "sspi") || CompareInsensitiveInvariant(stringValue, "true")) || CompareInsensitiveInvariant(stringValue, "yes"))
            {
                return true;
            }
            if (!CompareInsensitiveInvariant(stringValue, "false") && !CompareInsensitiveInvariant(stringValue, "no"))
            {
                string strvalue = stringValue.Trim();
                if ((CompareInsensitiveInvariant(strvalue, "sspi") || CompareInsensitiveInvariant(strvalue, "true")) || CompareInsensitiveInvariant(strvalue, "yes"))
                {
                    return true;
                }
                if (!CompareInsensitiveInvariant(strvalue, "false") && !CompareInsensitiveInvariant(strvalue, "no"))
                {
                    throw new ArgumentException(String.Format("Value {0} is an invalid connection option value. Integrated security.", stringValue));
                }
            }
            return false;
        }

        public string ConvertValueToString(string keyName, string defaultValue)
        {
            string str = (string)this._parsetable[keyName];
            if (str == null)
            {
                return defaultValue;
            }
            return str;
        }

        protected internal virtual PermissionSet CreatePermissionSet()
        {
            return null;
        }

        internal void DemandPermission()
        {
            if (this._permissionset == null)
            {
                this._permissionset = this.CreatePermissionSet();
            }
            this._permissionset.Demand();
        }


        private static string GetKeyName(StringBuilder buffer)
        {
            int length = buffer.Length;
            while ((0 < length) && char.IsWhiteSpace(buffer[length - 1]))
            {
                length--;
            }
            return buffer.ToString(0, length).ToLower(CultureInfo.InvariantCulture);
        }

        private static string GetKeyValue(StringBuilder buffer, bool trimWhitespace)
        {
            int length = buffer.Length;
            int startIndex = 0;
            if (trimWhitespace)
            {
                while ((startIndex < length) && char.IsWhiteSpace(buffer[startIndex]))
                {
                    startIndex++;
                }
                while ((0 < length) && char.IsWhiteSpace(buffer[length - 1]))
                {
                    length--;
                }
            }
            return buffer.ToString(startIndex, length - startIndex);
        }

        internal static int GetKeyValuePair(string connectionString, int currentPosition, StringBuilder buffer, bool useOdbcRules, out string keyname, out string keyvalue)
        {
            int index = currentPosition;
            buffer.Length = 0;
            keyname = null;
            keyvalue = null;
            char c = '\0';
            ParserState nothingYet = ParserState.NothingYet;
            int length = connectionString.Length;
            while (currentPosition < length)
            {
                c = connectionString[currentPosition];
                switch (nothingYet)
                {
                    case ParserState.NothingYet:
                        if ((';' != c) && !char.IsWhiteSpace(c))
                        {
                            if (c != '\0')
                            {
                                break;
                            }
                            nothingYet = ParserState.NullTermination;
                        }
                        goto Label_0255;

                    case ParserState.Key:
                        if ('=' != c)
                        {
                            goto Label_00BD;
                        }
                        nothingYet = ParserState.KeyEqual;
                        goto Label_0255;

                    case ParserState.KeyEqual:
                        if (useOdbcRules || ('=' != c))
                        {
                            goto Label_00E9;
                        }
                        nothingYet = ParserState.Key;
                        goto Label_024D;

                    case ParserState.KeyEnd:
                        goto Label_010C;

                    case ParserState.UnquotedValue:
                        if (char.IsWhiteSpace(c) || (!char.IsControl(c) && (';' != c)))
                        {
                            goto Label_024D;
                        }
                        goto Label_0262;

                    case ParserState.DoubleQuoteValue:
                        if ('"' != c)
                        {
                            goto Label_0192;
                        }
                        nothingYet = ParserState.DoubleQuoteValueQuote;
                        goto Label_0255;

                    case ParserState.DoubleQuoteValueQuote:
                        if ('"' != c)
                        {
                            goto Label_01AB;
                        }
                        nothingYet = ParserState.DoubleQuoteValue;
                        goto Label_024D;

                    case ParserState.SingleQuoteValue:
                        if ('\'' != c)
                        {
                            goto Label_01C7;
                        }
                        nothingYet = ParserState.SingleQuoteValueQuote;
                        goto Label_0255;

                    case ParserState.SingleQuoteValueQuote:
                        if ('\'' != c)
                        {
                            goto Label_01DD;
                        }
                        nothingYet = ParserState.SingleQuoteValue;
                        goto Label_024D;

                    case ParserState.BraceQuoteValue:
                        if ('}' != c)
                        {
                            goto Label_01F6;
                        }
                        nothingYet = ParserState.BraceQuoteValueQuote;
                        goto Label_024D;

                    case ParserState.BraceQuoteValueQuote:
                        if ('}' != c)
                        {
                            goto Label_020A;
                        }
                        nothingYet = ParserState.BraceQuoteValue;
                        goto Label_024D;

                    case ParserState.QuotedValueEnd:
                        goto Label_0217;

                    case ParserState.NullTermination:
                        if ((c != '\0') && !char.IsWhiteSpace(c))
                        {
                            throw new ArgumentException(String.Format("ConnectionStringSyntax {0} error", currentPosition));
                        }
                        goto Label_0255;

                    default:
                        throw new ArgumentException("Invalid Parser State1.");
                }
                if (char.IsControl(c))
                {
                    throw new ArgumentException(String.Format("ConnectionStringSyntax {0} error", index));
                }
                index = currentPosition;
                if ('=' != c)
                {
                    nothingYet = ParserState.Key;
                    goto Label_024D;
                }
                nothingYet = ParserState.KeyEqual;
                goto Label_0255;
            Label_00BD:
                if (char.IsWhiteSpace(c) || !char.IsControl(c))
                {
                    goto Label_024D;
                }
                throw new ArgumentException(String.Format("ConnectionStringSyntax {0} error", index));
            Label_00E9:
                keyname = GetKeyName(buffer);
                if (String.IsNullOrEmpty(keyname))
                {
                    throw new ArgumentException(String.Format("ConnectionStringSyntax {0} error", index));
                }
                buffer.Length = 0;
                nothingYet = ParserState.KeyEnd;
            Label_010C:
                if (char.IsWhiteSpace(c))
                {
                    goto Label_0255;
                }
                if (useOdbcRules)
                {
                    if ('{' != c)
                    {
                        goto Label_013F;
                    }
                    nothingYet = ParserState.BraceQuoteValue;
                    goto Label_024D;
                }
                if ('\'' == c)
                {
                    nothingYet = ParserState.SingleQuoteValue;
                    goto Label_0255;
                }
                if ('"' == c)
                {
                    nothingYet = ParserState.DoubleQuoteValue;
                    goto Label_0255;
                }
            Label_013F:
                if ((';' == c) || (c == '\0'))
                {
                    break;
                }
                if (char.IsControl(c))
                {
                    throw new ArgumentException(String.Format("ConnectionStringSyntax {0} error", index));
                }
                nothingYet = ParserState.UnquotedValue;
                goto Label_024D;
            Label_0192:
                if (c != '\0')
                {
                    goto Label_024D;
                }
                throw new ArgumentException(String.Format("ConnectionStringSyntax {0} error", index));
            Label_01AB:
                keyvalue = GetKeyValue(buffer, false);
                nothingYet = ParserState.QuotedValueEnd;
                goto Label_0217;
            Label_01C7:
                if (c != '\0')
                {
                    goto Label_024D;
                }
                throw new ArgumentException(String.Format("ConnectionStringSyntax {0} error", index));
            Label_01DD:
                keyvalue = GetKeyValue(buffer, false);
                nothingYet = ParserState.QuotedValueEnd;
                goto Label_0217;
            Label_01F6:
                if (c != '\0')
                {
                    goto Label_024D;
                }
                throw new ArgumentException(String.Format("ConnectionStringSyntax {0} error", index));
            Label_020A:
                keyvalue = GetKeyValue(buffer, false);
                nothingYet = ParserState.QuotedValueEnd;
            Label_0217:
                if (char.IsWhiteSpace(c))
                {
                    goto Label_0255;
                }
                if (';' == c)
                {
                    break;
                }
                if (c == '\0')
                {
                    nothingYet = ParserState.NullTermination;
                    goto Label_0255;
                }
                throw new ArgumentException(String.Format("ConnectionStringSyntax {0} error", index));
            Label_024D:
                buffer.Append(c);
            Label_0255:
                currentPosition++;
            }
        Label_0262:
            switch (nothingYet)
            {
                case ParserState.NothingYet:
                case ParserState.KeyEnd:
                case ParserState.NullTermination:
                    break;

                case ParserState.Key:
                case ParserState.DoubleQuoteValue:
                case ParserState.SingleQuoteValue:
                case ParserState.BraceQuoteValue:
                    throw new ArgumentException(String.Format("ConnectionStringSyntax {0} error", index));

                case ParserState.KeyEqual:
                    keyname = GetKeyName(buffer);
                    if (String.IsNullOrEmpty(keyname))
                    {
                        throw new ArgumentException(String.Format("ConnectionStringSyntax {0} error", index));
                    }
                    break;

                case ParserState.UnquotedValue:
                    {
                        keyvalue = GetKeyValue(buffer, true);
                        char ch2 = keyvalue[keyvalue.Length - 1];
                        if (!useOdbcRules && (('\'' == ch2) || ('"' == ch2)))
                        {
                            throw new ArgumentException(String.Format("ConnectionStringSyntax {0} error", index));
                        }
                        break;
                    }
                case ParserState.DoubleQuoteValueQuote:
                case ParserState.SingleQuoteValueQuote:
                case ParserState.BraceQuoteValueQuote:
                case ParserState.QuotedValueEnd:
                    keyvalue = GetKeyValue(buffer, false);
                    break;

                default:
                    throw new ArgumentException("InvalidParserState2");
            }
            if ((';' == c) && (currentPosition < connectionString.Length))
            {
                currentPosition++;
            }
            return currentPosition;
        }

        private static bool IsKeyNameValid(string keyname)
        {
            if (keyname == null)
            {
                return false;
            }
            return ((((0 < keyname.Length) && (';' != keyname[0])) && !char.IsWhiteSpace(keyname[0])) && (-1 == keyname.IndexOf('\0')));
        }

        private static bool IsValueValidInternal(string keyvalue)
        {
            if (keyvalue != null)
            {
                return (-1 == keyvalue.IndexOf('\0'));
            }
            return true;
        }

        private static NameValuePair ParseInternal(Hashtable parsetable, string connectionString, bool buildChain, Hashtable synonyms, bool firstKey)
        {
            StringBuilder buffer = new StringBuilder();
            NameValuePair pair = null;
            NameValuePair pair2 = null;
            int num = 0;
            int length = connectionString.Length;
            while (num < length)
            {
                string str2;
                string str3;
                int currentPosition = num;
                num = GetKeyValuePair(connectionString, currentPosition, buffer, firstKey, out str2, out str3);
                if (String.IsNullOrEmpty(str2))
                {
                    return pair2;
                }
                string keyname = (synonyms != null) ? ((string)synonyms[str2]) : str2;
                if (!IsKeyNameValid(keyname))
                {
                    throw new NotSupportedException(String.Format("KeyName {0}", str2));
                }
                if (!firstKey || !parsetable.Contains(keyname))
                {
                    parsetable[keyname] = str3;
                }
                if (pair != null)
                {
                    pair = pair.Next = new NameValuePair(keyname, str3, num - currentPosition);
                }
                else if (buildChain)
                {
                    pair2 = pair = new NameValuePair(keyname, str3, num - currentPosition);
                }
            }
            return pair2;
        }

        internal NameValuePair ReplacePasswordPwd(out string constr, bool fakePassword)
        {
            int startIndex = 0;
            NameValuePair pair4 = null;
            NameValuePair pair3 = null;
            NameValuePair pair2 = null;
            StringBuilder builder = new StringBuilder(this._usersConnectionString.Length);
            for (NameValuePair pair = this.KeyChain; pair != null; pair = pair.Next)
            {
                if (("password" != pair.Name) && ("pwd" != pair.Name))
                {
                    builder.Append(this._usersConnectionString, startIndex, pair.Length);
                    if (fakePassword)
                    {
                        pair2 = new NameValuePair(pair.Name, pair.Value, pair.Length);
                    }
                }
                else if (fakePassword)
                {
                    builder.Append(pair.Name).Append("=*;");
                    pair2 = new NameValuePair(pair.Name, "*", pair.Name.Length + "=*;".Length);
                }
                if (fakePassword)
                {
                    if (pair3 != null)
                    {
                        pair3 = pair3.Next = pair2;
                    }
                    else
                    {
                        pair3 = pair4 = pair2;
                    }
                }
                startIndex += pair.Length;
            }
            constr = builder.ToString();
            return pair4;
        }

        public string UsersConnectionString(bool hidePassword)
        {
            return this.UsersConnectionString(hidePassword, false);
        }

        private string UsersConnectionString(bool hidePassword, bool forceHidePassword)
        {
            string constr = this._usersConnectionString;
            if (this.HasPasswordKeyword && (forceHidePassword || (hidePassword && !this.HasPersistablePassword)))
            {
                this.ReplacePasswordPwd(out constr, false);
            }
            if (constr == null)
            {
                return "";
            }
            return constr;
        }

        internal string UsersConnectionStringForTrace()
        {
            return this.UsersConnectionString(true, true);
        }

        internal static void ValidateKeyValuePair(string keyword, string value)
        {
            if ((keyword == null) || !ConnectionStringValidKeyRegex.IsMatch(keyword))
            {
                throw new ArgumentException(String.Format("{0} key name is invalid.", keyword), keyword);
            }
            if ((value != null) && !ConnectionStringValidValueRegex.IsMatch(value))
            {
                throw new ArgumentException(String.Format("{0} key value is invalid.", keyword), keyword);
            }
        }

        // Properties
        internal bool HasBlankPassword
        {
            get
            {
                if (this.ConvertValueToIntegratedSecurity())
                {
                    return false;
                }
                if (this._parsetable.ContainsKey("password"))
                {
                    return String.IsNullOrEmpty((string)this._parsetable["password"]);
                }
                if (this._parsetable.ContainsKey("pwd"))
                {
                    return String.IsNullOrEmpty((string)this._parsetable["pwd"]);
                }
                return ((this._parsetable.ContainsKey("user id") && !String.IsNullOrEmpty((string)this._parsetable["user id"])) || (this._parsetable.ContainsKey("uid") && !String.IsNullOrEmpty((string)this._parsetable["uid"])));
            }
        }

        internal bool HasPersistablePassword
        {
            get
            {
                if (this.HasPasswordKeyword)
                {
                    return this.ConvertValueToBoolean("persist security info", false);
                }
                return true;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return (null == this.KeyChain);
            }
        }

        public string this[string keyword]
        {
            get
            {
                return (string)this._parsetable[keyword];
            }
        }

        public ICollection Keys
        {
            get
            {
                return this._parsetable.Keys;
            }
        }

        internal Hashtable Parsetable
        {
            get
            {
                return this._parsetable;
            }
        }

        // Nested Types
        private enum ParserState
        {
            BraceQuoteValue = 10,
            BraceQuoteValueQuote = 11,
            DoubleQuoteValue = 6,
            DoubleQuoteValueQuote = 7,
            Key = 2,
            KeyEnd = 4,
            KeyEqual = 3,
            NothingYet = 1,
            NullTermination = 13,
            QuotedValueEnd = 12,
            SingleQuoteValue = 8,
            SingleQuoteValueQuote = 9,
            UnquotedValue = 5
        }
    }


}
