#!/usr/bin/env ruby

require 'cielo24'
require_relative "../lib/cielo24_command/application"
include Cielo24Command

Application.start(ARGV)