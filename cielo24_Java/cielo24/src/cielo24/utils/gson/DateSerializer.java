package cielo24.utils.gson;

import java.lang.reflect.Type;
import java.util.Date;

import cielo24.Utils;

import com.google.gson.JsonElement;
import com.google.gson.JsonPrimitive;
import com.google.gson.JsonSerializationContext;
import com.google.gson.JsonSerializer;

public class DateSerializer implements JsonSerializer<Date>{

	@Override
	public JsonElement serialize(Date date, Type type, JsonSerializationContext context) {
		return new JsonPrimitive(Utils.dateFormat.format(date));
	}
}