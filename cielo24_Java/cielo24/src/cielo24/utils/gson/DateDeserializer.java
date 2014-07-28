package cielo24.utils.gson;

import java.lang.reflect.Type;
import java.text.ParseException;
import java.util.Date;

import cielo24.Utils;

import com.google.gson.JsonDeserializationContext;
import com.google.gson.JsonDeserializer;
import com.google.gson.JsonElement;
import com.google.gson.JsonParseException;

public class DateDeserializer implements JsonDeserializer<Date> {

    @Override
    public Date deserialize(JsonElement json, Type type, JsonDeserializationContext context) throws JsonParseException {
    	if(json.getAsString().equals("")){
    		return null;
    	}
    	else {
    		try {
				return Utils.dateFormat.parse(json.getAsString());
			} catch (ParseException e) {
				return null;
			}
    	}
    }
}