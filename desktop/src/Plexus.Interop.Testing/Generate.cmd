CALL plexus gen-json-meta -b TestBrokerConfig/metadata/interop -o TestBrokerConfig/metadata --verbose
CALL plexus gen-csharp -b TestBrokerConfig/metadata/interop -o Generated -i {echo_client.interop,echo_server.interop,test_app_launcher.interop} -n Plexus.Interop.Testing.Generated --verbose