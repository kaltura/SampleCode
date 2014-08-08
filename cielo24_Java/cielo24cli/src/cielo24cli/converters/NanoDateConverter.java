package cielo24cli.converters;

import java.text.ParseException;

import cielo24.utils.NanoDate;

import com.beust.jcommander.IStringConverter;

public class NanoDateConverter implements IStringConverter<NanoDate> {
	@Override
	public NanoDate convert(String arg) {
		try {
			return NanoDate.parse(arg);
		} catch (ParseException e) {
			return null;
		}
	}
}