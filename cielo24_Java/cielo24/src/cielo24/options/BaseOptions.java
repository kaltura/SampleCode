package cielo24.options;

import java.lang.reflect.Field;
import java.lang.reflect.Type;
import java.util.ArrayList;
import java.util.Date;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

import com.google.common.base.Objects;

import cielo24.Utils;
import cielo24.utils.Dictionary;
import cielo24.utils.KeyValuePair;
import cielo24.utils.QueryName;

/* The base class. All of the other option classes inherit from it. */
public abstract class BaseOptions {

	/* Returns a dictionary that contains key-value pairs of options, where key is the Name property
     * of the QueryName attribute assigned to every option and value is the value of the property.
     * Options with null value are not included in the dictionary. */
    public Dictionary<String, String> GetDictionary() throws IllegalArgumentException, IllegalAccessException {
        Dictionary<String, String> queryDictionary = new Dictionary<String, String>();
        Field[] fields = this.getClass().getDeclaredFields();
        for (Field field : fields) {
        	Object value = field.get(this);
            if (value != null) { // If field is null, don't include the key-value pair in the dictioanary
                QueryName key = field.getDeclaredAnnotation(QueryName.class);
                queryDictionary.add(key.value(), this.getStringValue(value));
            }
        }
        return queryDictionary;
    }

    /* Returns a query String representation of options */
    public String toQuery() throws IllegalArgumentException, IllegalAccessException {
        Dictionary<String, String> queryDictionary = this.GetDictionary();
        return Utils.toQuery(queryDictionary);
    }

    /* Sets the property whose QueryName attribute matches the key */
    public void populateFromKeyValuePair(KeyValuePair<String, String> pair) throws IllegalArgumentException, IllegalAccessException {
    	Field[] fields = this.getClass().getDeclaredFields();
        for (Field field : fields) {
        	QueryName key = field.getDeclaredAnnotation(QueryName.class);
            Type type = field.getType();
            if (key.value().equals(pair.key)) {
            	field.set(this, this.getValueFromString(pair.value, type));
				return;
			}
        }
        throw new IllegalArgumentException("Invalid option: " + pair.key); // Fail if property not found
    }

    // Array of Strings in the key=value form
    public void populateFromArray(String[] array) throws IllegalArgumentException, IllegalAccessException {
        for (String s : Objects.firstNonNull(array, new String[0])) {
        	Matcher regex = Pattern.compile("([^?=&]+)(=([^&]*))?").matcher(s);
        	regex.matches();
        	this.populateFromKeyValuePair(new KeyValuePair<String, String>(regex.group(1), regex.group(3)));
        }
    }

    /* Converts String into an object */
    protected Object getValueFromString(String str, Type type) {
    	// Quotes are necessary in json
    	return Utils.deserialize("\"" + str + "\"", type);
    }

    /* Converts 'value' into String based on its type. Precondition: value != null */
    protected String getStringValue(Object value) {
        if (value instanceof ArrayList<?>) {
        	// ArrayLists can contain strings and enums, type does not matter
            return Utils.joinQuoteList((ArrayList<?>)value, ", ");
        } else if (value instanceof char[]) {   // char[] (returned as (a, b))
            return Utils.joinCharArray((char[])value, ", ");
        } else if (value instanceof Date) {     // DateTime (in ISO 8601 format)
        	return Utils.dateFormat.format((Date)value);
        } else {                                // Takes care of the rest: Integer, Boolean, String, URL
            return value.toString();
        }
    }
}