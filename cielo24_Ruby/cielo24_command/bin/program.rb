#!/usr/bin/env ruby

require 'cielo24'
require_relative "../lib/cielo24_command/application"
include Cielo24Command

begin
  Application.start(ARGV)
rescue ArgumentError => e
  puts "ERROR: " + e.message
  exit(1)
rescue Cielo24::WebError => e
  puts "ERROR: " + e.message
  exit(1)
end