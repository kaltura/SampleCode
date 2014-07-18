require 'test/unit'
require '../lib/RubyWrapperCielo24/actions'
require '../lib/RubyWrapperCielo24/web_utils'
include RubyWrapperCielo24

class AccessTest < Test::Unit::TestCase

  # Called before every test method runs. Can be used
  # to set up fixture information.
  def setup
    @actions = RubyWrapperCielo24::Actions.new
    @actions.base_url = "http://sandbox-dev.cielo24.com"
    @username = "testscript"
    @password = "testscript2"
    @new_password = "testscript"
    @api_token = @actions.login(@username, @password, nil, true)
  end

  def test_login_and_logout
    # Username, password, no headers
    assert_nothing_raised{@api_token = @actions.login(@username, @password)}
    # Username, password, headers
    assert_nothing_raised{@api_token = @actions.login(@username, @password, nil, true)}
    @secure_key = @actions.generate_api_key(@api_token, @username, false)
    # Username, secure key, no headers
    assert_nothing_raised{@api_token = @actions.login(@username, nil, @secure_key)}
    # Username, secure key, headers
    assert_nothing_raised{@api_token = @actions.login(@username, nil, @secure_key, true)}
    # Logout
    assert_nothing_raised{@actions.logout(@api_token)}
  end

  def test_generate_and_remove_api_key
    assert_nothing_raised{@secure_key = @actions.generate_api_key(@api_token, @username, false)}
    assert_nothing_raised{@secure_key = @actions.generate_api_key(@api_token, @username, true)}
    assert_nothing_raised{@actions.remove_api_key(@api_token, @secure_key)}
  end

  def test_update_password
    assert_nothing_raised{@actions.update_password(@api_token, @new_password)}
    assert_nothing_raised{@actions.update_password(@api_token, @password)}
  end

  def test_create_job
    assert_nothing_raised{@actions.create_job(@api_token,"testname","en")}
  end

  # Called after every test method runs. Can be used to tear
  # down fixture information.
  def teardown
    # Do nothing
  end
end

class JobTest < Test::Unit::TestCase

end