# coding: utf-8
lib = File.expand_path('../lib', __FILE__)
$LOAD_PATH.unshift(lib) unless $LOAD_PATH.include?(lib)
require 'cielo24_command/version'

Gem::Specification.new do |spec|
  spec.name          = "cielo24-cli"
  spec.version       = Cielo24Command::VERSION
  spec.authors       = ["Evgeny Chistyakov"]
  spec.email         = ["support@cielo24.com"]
  spec.summary       = %q{Command line interface to cielo24 gem.}
  spec.description   = %q{Command line interface that allows you to make web API calls using cielo24 gem.}
  spec.homepage      = "http://cielo24.com"
  spec.license       = "MIT"

  spec.files         = Dir['lib/cielo24_command/*.rb'] + Dir['lib/*.rb']
  spec.executables   = ["cielo24"] #spec.files.grep(%r{^bin/}) { |f| File.basename(f) }
  spec.test_files    = spec.files.grep(%r{^(test|spec|features)/})
  spec.require_paths = ["lib"]

  spec.add_development_dependency "bundler", "~> 1.6"
  spec.add_development_dependency "rake", "~> 10.0"

  spec.add_dependency "cielo24"
  spec.add_dependency "thor"
end
