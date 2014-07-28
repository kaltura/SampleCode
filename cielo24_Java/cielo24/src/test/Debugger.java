package test;
import cielo24.Actions;


public class Debugger {

	public static void main(String[] args) throws Exception {
		Actions a = new Actions();
		a.serverUrl = "http://sandbox-dev.cielo24.com";
		System.out.print(a.login("testscript", "testscript2").toString());
	}
}