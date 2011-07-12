/*
 * MvcJsI18n is created by Larry Zhao.
 * Repository is at https://github.com/larryzhao/MvcJsI18n
 * Home page is at http://larryzhao.com/blog/ursacoder-mvcjsi18n
 * 
 * This is an opensource project, under license EPL.
 * You could use it in anywhere you want.
 * 
 * Thank you very much. :)
 * 
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace UrsaCoder.MvcJsI18n.Config
{
    class JsI18nConfigHandler : ConfigurationSection
    {
        // Create a "defaultLocale" attribute.
        [ConfigurationProperty("defaultLocale", IsRequired = true)]
        public DefaultLocaleElement DefaultLocale
        {
            get { return (DefaultLocaleElement)this["defaultLocale"]; }
            set { this["defaultLocale"] = value; }
        }

        // Create a "jsOutputPath" element.
        [ConfigurationProperty("defaultTargetPath", IsRequired = true)]
        public DefaultTargetPathElement DefaultTargetPath
        {
            get
            {
                return (DefaultTargetPathElement)this["defaultTargetPath"];
            }
            set
            { this["defaultTargetPath"] = value; }
        }

        [ConfigurationProperty("defaultSourcePath", IsRequired = true)]
        public DefaultSourcePathElement DefaultSourcePath
        {
            get
            {
                return (DefaultSourcePathElement)this["defaultSourcePath"];
            }
            set
            { this["defaultSourcePath"] = value; }
        }

        [ConfigurationProperty("operation", IsRequired = true)]
        public OperationElement Operation
        {
            get { return (OperationElement)this["operation"]; }
            set { this["operation"] = value; }
        }


        [ConfigurationProperty("useShortLocale", IsRequired = true)]
        public UseShortLocaleElement UseShortLocale
        {
            get { return (UseShortLocaleElement)this["useShortLocale"]; }
            set { this["useShortLocale"] = value; }
        }


        //Configuration Elements:
        public class DefaultLocaleElement : ConfigurationElement
        {
            [ConfigurationProperty("value", IsRequired = true)]
            public String Value
            {
                get { return (String)this["value"]; }
                set { this["value"] = value; }
            }
        }

        public class DefaultTargetPathElement : ConfigurationElement
        {
            [ConfigurationProperty("value", IsRequired = true)]
            public String Value
            {
                get { return (String)this["value"]; }
                set { this["value"] = value; }
            }
        }

        public class DefaultSourcePathElement : ConfigurationElement
        {
            [ConfigurationProperty("value", IsRequired = true)]
            public String Value
            {
                get { return (String)this["value"]; }
                set { this["value"] = value; }
            }
        }

        public class OperationElement : ConfigurationElement
        {
            [ConfigurationProperty("value", IsRequired = true)]
            public String Value
            {
                get { return (String)this["value"]; }
                set { this["value"] = value; }
            }
        }

        public class UseShortLocaleElement : ConfigurationElement
        {
            [ConfigurationProperty("value", IsRequired = true)]
            public Boolean Value
            {
                get { return (Boolean)this["value"]; }
                set { this["value"] = value; }
            }
        }
    }
}
